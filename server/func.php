<?php
require('./config.php');

function connect() {
	$link = mysql_connect(DB_HOST, DB_USER, DB_PSWD);
	if (!$link) {
		die('connection error'.mysql_error());
	}
	return $link;
}

function disconnect($link) {
	$close_flag = mysql_close($link);
}

function select($link, $query) {
	$db_selected = mysql_select_db(DB_NAME, $link);
	if (!$db_selected){
		die('DB select error'.mysql_error());
	}
	mysql_set_charset('utf8');
	$result = mysql_query($query);
	if (!$result) {
		die('query error'.mysql_error());
	}
	$array = [];
	while ($row = mysql_fetch_assoc($result)) {
		$array[] = $row;
	}
	return $array;
}

function insert($link, $query) {
	$db_selected = mysql_select_db(DB_NAME, $link);
	if (!$db_selected){
		die('DB select error'.mysql_error());
	}
	mysql_set_charset('utf8');
	$result = mysql_query($query);
	if (!$result) {
		return false;
	}
	return true;
}
