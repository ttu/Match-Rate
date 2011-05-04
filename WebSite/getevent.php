<?php
$ip = ((isset($_SERVER['HTTP_X_FORWARDED_FOR'])) ? $_SERVER['HTTP_X_FORWARDED_FOR'] : $_SERVER['REMOTE_ADDR']);
$eventID = $_GET["Id"];

// Validate variables
if(!filter_var($ip, FILTER_VALIDATE_IP)){ $ip = "0.0.0.0"; }
if (!is_numeric($eventID)) { $eventID = 0; }

$query = "SELECT ID as EventID, Name, Date, fight.FightID, Fighter1_ID, Fighter1_Name, Fighter1_Url, Fighter2_ID, Fighter2_Name, Fighter2_Url, COALESCE(Up,0) as Up, COALESCE(Down,0) as Down, COALESCE(ipRate.Rate, -1) AS IpRate FROM Event 
LEFT JOIN 
	(SELECT ID AS FightID, EventID,Fighter_1_ID,Fighter_2_ID FROM Fight) fight 
ON Event.ID = fight.EventID 
LEFT JOIN 
	(SELECT ID as Fighter1_ID, Name as Fighter1_Name, Url as Fighter1_Url FROM Fighter) f1 
ON Fighter_1_ID = f1.Fighter1_ID
LEFT JOIN 
	(SELECT ID as Fighter2_ID, Name as Fighter2_Name, Url as Fighter2_Url FROM Fighter) f2 
ON Fighter_2_ID = f2.Fighter2_ID 
LEFT JOIN 
	(SELECT rate1.FightID, Up, Down FROM 
		(SELECT Rating.*, COUNT(*) AS Up FROM Rating 
		LEFT JOIN Fight ON Rating.FightID = Fight.ID 
		WHERE Fight.EventID = $eventID AND Rating.Rate = 1 
		GROUP BY Rating.FightID) rate1 
	LEFT JOIN
		(SELECT Rating.*, COUNT(*) AS Down FROM Rating 
		LEFT JOIN Fight ON Rating.FightID = Fight.ID 
		WHERE Fight.EventID = $eventID AND Rating.Rate = 0 
		GROUP BY Rating.FightID) rate2 
	ON rate2.FightID = rate1.FightID
	UNION
	SELECT rate2.FightID, Up, Down FROM 
		(SELECT Rating.*, COUNT(*) AS Up FROM Rating 
		LEFT JOIN Fight ON Rating.FightID = Fight.ID 
		WHERE Fight.EventID = $eventID AND Rating.Rate = 1 
		GROUP BY Rating.FightID) rate1 
	RIGHT JOIN
		(SELECT Rating.*, COUNT(*) AS Down FROM Rating 
		LEFT JOIN Fight ON Rating.FightID = Fight.ID 
		WHERE Fight.EventID = $eventID AND Rating.Rate = 0 
		GROUP BY Rating.FightID) rate2 
	ON rate2.FightID = rate1.FightID) r1 
ON r1.FightID = fight.FightID
LEFT JOIN
	(SELECT FightID, Rate FROM Rating
	WHERE IP = '$ip') ipRate
ON ipRate.FightID = fight.FightID
WHERE EventID = $eventID";

$url = "";
$userName = "";
$password = "";
$dbName = "";

$link = mysql_pconnect($url, $userName, $password) or die("Could not connect");
mysql_select_db($dbName) or die("Could not select database");
 
$arr = array();
$rs = mysql_query($query);
 
 $json = "{\"events\": [ \r\n\t\t";
 $first = true;
 
 while ($row = mysql_fetch_assoc($rs)) {
 
   if ($first){
	$id = $row['EventID'];
	$name = $row['Name'];
	$date = $row['Date'];
	$first = false;
	$json .= "{\r\n\t\t\t\"id\": $id,\r\n\t\t\t\"name\": \"$name\",\r\n\t\t\t\"date\": \"$date\",\r\n\t\t\t\"fights\": [";
   }
   
   $fid =  $row['FightID'];
   $ipRate =  $row['IpRate'];
   $up =  $row['Up'];
   $down =  $row['Down'];
   $f1_name =  $row['Fighter1_Name'];
   $f2_name =  $row['Fighter2_Name'];
   $f1_url =  $row['Fighter1_Url'];
   $f2_url =  $row['Fighter2_Url'];
   
   $json .= "\r\n\t\t\t\t\t{\r\n\t\t\t\t\t\t\"id\": $fid,\r\n\t\t\t\t\t\t\"ipvote\": $ipRate,\r\n\t\t\t\t\t\t\"fighter1\": {\r\n\t\t\t\t\t\t\t\"name\": \"$f1_name\",\r\n\t\t\t\t\t\t\t\"url\": \"$f1_url\"\r\n\t\t\t\t\t\t},\r\n\t\t\t\t\t\t\"fighter2\" : {\r\n\t\t\t\t\t\t\t\"name\": \"$f2_name\",\r\n\t\t\t\t\t\t\t\"url\": \"$f2_url\"\r\n\t\t\t\t\t\t},\r\n\t\t\t\t\t\t\"up\": $up,\r\n\t\t\t\t\t\t\"down\": $down\r\n\t\t\t\t\t},";
}

$json = substr($json, 0, -1);

$json .= "\r\n\t\t\t\t]\r\n\t\t}\r\n]}";
echo "$json";

//while($obj = mysql_fetch_object($rs)) {	
//	$arr[] = $obj;
//}
//echo '{"Events":'.json_encode($arr).'}';
?>