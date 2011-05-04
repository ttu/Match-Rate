<?php
$url = "";
$userName = "";
$password = "";
$dbName = "";

$link = mysql_pconnect($url, $userName, $password) or die("Could not connect");
mysql_select_db($dbName) or die("Could not select database");

$query = "SELECT * FROM Event ORDER BY Date DESC";
 
$json = "{\"events\": [ \r\n\t\t";

$rs = mysql_query($query);
 
 while ($row = mysql_fetch_assoc($rs)) {
	$id = $row['ID'];
	$name = $row['Name'];
	$date = $row['Date'];
	$json .= "{\r\n\t\t\t\"id\": $id,\r\n\t\t\t\"name\": \"$name\",\r\n\t\t\t\"date\": \"$date\"\r\n\t\t},";
}

// Drop off last ,
$json = substr($json, 0, -1);

$json .= "\r\n]}";
echo "$json";
?>