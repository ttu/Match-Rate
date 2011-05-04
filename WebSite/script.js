$(document).ready(function() {
    $('#menu').toggleClass('hide');

	$.getJSON('getevents.php', function(data) {
		processContent(data, true);		
		initializeEvent();
		// Open first event by default
		$('#container .header:first').click();
	});
});

function processContent(data, fullData){
		// Go through all events in JSON data
		$.each(data.events, function(event) {

			var arrowImage = $('<img/>');
			var contentDiv = $('<div>', {'class': 'content'});
			
			// Fights might be null when just bringing titles of events
			if (this.fights != null){
				// If has fights then arrows is pointing down, cos div will be open by default
				arrowImage.attr('src', 'images/arrow_up.gif');
				
				var contentList = $('<ul>');
				
				if (!fullData)
					contentList.attr('style', 'display: none;');
					
				contentDiv.append(contentList);

				$.each(this.fights, function(fight) {
					
					var fightListElement = $('<li>');
					contentList.append(fightListElement);
					
					var fightDiv = $('<div>',{
					'id': 'f_' + this.id,
					'class': 'fight'
					});
					fightListElement.append(fightDiv);
					
					if (this.ipvote != null)
					{
						if (this.ipvote == 1)
							fightDiv.attr('class', 'fight_voteup');
						else if (this.ipvote == 0)
							fightDiv.attr('class', 'fight_votedown');
					}
					
					var fightTitleSpan = $('<span>',{'class': 'fighttitle'});
					fightDiv.append(fightTitleSpan);
					
					var fighter1 = $('<a>',{'href': this.fighter1.url, html: this.fighter1.name});
					var fighter2 = $('<a>',{'href': this.fighter2.url, html: this.fighter2.name});
					
					fightTitleSpan.append(fighter1);
					fightTitleSpan.append(' - ');
					fightTitleSpan.append(fighter2);
					
					var dislikeSpan = $('<span>',{'class': 'dislike'}); 
					var dislikeCount = $('<span>', {'class': 'clickcount', html: this.down});
					dislikeSpan.append(dislikeCount);
					
					var likeSpan = $('<span>',{'class': 'like'});
					var likeCount = $('<span>', {'class': 'clickcount', html: this.up});
					likeSpan.append(likeCount);
					
					fightDiv.append(dislikeSpan);
					fightDiv.append(likeSpan);
				});
			}
			else{
				arrowImage.attr('src', 'images/arrow_down.gif');
			}
						
			// Fulldata also adds event title div
			if (fullData){
			
				var mainDiv = $('<div/>',{
					'id': this.id,
					'class': 'event'
				}).appendTo('#container');
				
				
				var event = $('<div>',{
				'class': 'header',
				'onclick': 'hideContent('+this.id+')'});
				
				mainDiv.append(event);
				
				var title = $('<label>',{
				'class': 'eventname',
				html: this.name});
				
				var date = $('<label>',{
				'class': 'eventdate',
				html: this.date});
				
				event.append(title);
				event.append(date);
				event.append(arrowImage);

				mainDiv.append(contentDiv);
			}
			else{
				$('#'+ this.id).append(contentDiv);
			}
		});
};

function initializeEvent(){
		$('.fight .like').click(function() {
            // First parent is span .like and second is div
            voteClick($(this));
        });

        $('.fight .dislike').click(function() {
            voteClick($(this));
        });

		// TODO: Change cursor switching to better
        $('.fight .like').hover(function() {
            $(this).css('cursor', 'pointer');
        });

        $('.fight .dislike').hover(function() {
            $(this).css('cursor', 'pointer');
        });

        $('.fighttitle a').mouseover(function() {
            // TODO: Show layer with fighter info
            //showInfoBox(this);
        });
};

/*$.ajaxSetup({
    // Disable caching of AJAX responses 
    cache: false
});*/

// Show info layer of fighter
function showInfoBox(link) {
    var linkUrl = $(link).attr('href');
};

// Change color of background and unbind events
// Voting just raises number by one
// TODO: Check if voting exception should be shown to user
function voteClick(div) {
	
	$voteID = -1;
	
    // Change image
    if ($(div).attr('class') == "like") {
		$(div).parent().attr('class', 'fight_voteup');
		$voteID = 1;
    }
    else {
		$(div).parent().attr('class', 'fight_votedown');
		$voteID = 0;
    }
	
	var text = $('.clickCount', div)[0].innerHTML;
    var value = parseInt(text);
    $('.clickCount', div)[0].innerHTML = (value + 1);

	unbindControls($(div).parent());    
	
	// Need to drop off f_ from the beginning
	$id = $(div).parent().attr('id').substring(2);
	
	$.getJSON('vote.php?Id=' + $id + "&Vote=" + $voteID, function(data) {
		//processContent(data, true);		
		//initializeEvent();
	});
};

function unbindControls(div) {
	// Disable click
    $('.like', div).unbind('click');
    $('.dislike', div).unbind('click');

    // Unbind hover and clear style (takes pointer away)
	$('.like', div).unbind('mouseenter mouseleave');
	$('.dislike', div).unbind('mouseenter mouseleave');
    $('.like', div).attr('style', '');
    $('.dislike', div).attr('style', '');
};

// Hide/Show event content
function hideContent(id) {
	// Check if needs to get data from server
	if ($('#' + id + ' ul').length == 0){
		getNewContent(id);
		return;
	}
	
    if ($('#' + id + ' ul').is(":hidden")) {
			$('#' + id + ' ul').slideDown();
			var src = $('#' + id + ' .header img').attr('src').replace("_down", "_up");
			$('#' + id + ' .header img').attr('src', src);
		
    }
    else {
        $('#' + id + ' ul').slideUp();
        var src = $('#' + id + ' .header img').attr('src').replace("_up", "_down");
        $('#' + id + ' .header img').attr('src', src);
    }
};

function getNewContent(id){

	$.getJSON('getevent.php?Id=' + id, function(data) {
		processContent(data, false);		
		initializeEvent();
				
		$('#' + id + ' ul').slideDown();
		var src = $('#' + id + ' .header img').attr('src').replace("_down", "_up");
		$('#' + id + ' .header img').attr('src', src);
	});
};

// Show hide menu
function toggleMenu() {
    $('#menu').toggleClass('hide');
    $('#header .leftButton').toggleClass('pressed');
};


	
	
	
