<?php
function handleError($entry, $info) {
	$date = date("d.m.Y H:i");
	$file = fopen("voteErrorLog.txt", "a"); 
	fwrite($file, "$info: $entry\r\n");
	fclose($file);
	die("$info: $entry");
}
	
$ip = ((isset($_SERVER['HTTP_X_FORWARDED_FOR'])) ? $_SERVER['HTTP_X_FORWARDED_FOR'] : $_SERVER['REMOTE_ADDR']);
$id = $_GET["Id"];
$vote = $_GET["Vote"];
$date = date("Y-m-d H:i:s");
$currentTs = strtotime("now");

$infoString = $date." - ".$ip." - ".$id." - ".$vote;

// Validate variables
if(!filter_var($ip, FILTER_VALIDATE_IP)){ 
	handleError("Not valid IP", $infoString);
}
if (!is_numeric($id)){	
	handleError("Not valid ID", $infoString); 
}
if ($vote != 1 && $vote != 0) { 
	handleError("Not valid Vote", $infoString); 
}

$url = "";
$userName = "";
$password = "";
$dbName = "";

$link = mysql_pconnect($url, $userName, $password) or die("Could not connect");
mysql_select_db($dbName) or die("Could not select database");

// Check if ip voted this event
$query = "SELECT * FROM Rating WHERE IP = '$ip' AND FightID = $id";
$rs = mysql_query($query);

while ($row = mysql_fetch_assoc($rs)) {	
	echo($row);
	handleError("Double submit from same IP for ID", $infoString); 
}

// Check latest vote from this ip
$query = "SELECT * FROM Rating WHERE IP = '$ip' ORDER BY Date DESC LIMIT 1";
$rs = mysql_query($query);

while ($row = mysql_fetch_assoc($rs)) {
	$dt = $row['Date'];	
	$ts = strtotime($dt);
	echo("$ts : $currentTs \r\n");
	if ($currentTs - $ts < 1){ 
		handleError("Too fast submit from same IP", $infoString); 
	}
}

// Check latest votes
$query = "SELECT * FROM Rating Where FightID = $id ORDER BY Date DESC LIMIT 5";
$rs = mysql_query($query);

$compareTs = $currentTs;

while ($row = mysql_fetch_assoc($rs)) {
	$dt = $row['Date'];
	$ts = strtotime($dt);
	
	if ($compareTs - $ts < 2){ 
		handleError("Too fast submit for with same values", $infoString); 
	}
	
	$compareTs = $ts;
}

$query = "INSERT INTO Rating VALUES('$ip', $id, $vote, '$date')";
$rs = mysql_query($query);


?>