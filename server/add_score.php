<?php
require('./func.php');

if (empty($_POST['user_code'])) { return; }
if (empty($_POST['score'])) { return; }
if (empty($_POST['name'])) { return; }
// db connect
$link = connect();
header('Access-Control-Allow-Origin: *');
header('Access-Control-Allow-Headers: Origin, X-Requested-With, Content-Type, Accept');

// select
$query = 'SELECT * FROM users WHERE code = "' . $_POST['user_code'] . '";';
$result = select($link, $query);
$userId = 0;
if (count($result) == 0) {
	$query = 'INSERT INTO users (name, code) VALUES ("' . $_POST['name'] . '", "' . $_POST['user_code'] . '");';
	if (insert($link, $query))
		$userId = mysql_insert_id($link);
} else {
	$userId = $result[0]['id'];
}
if ($userId == 0) return;
$query = 'INSERT INTO scores (user_id, score) VALUES ("' . $userId . '", ' . $_POST['score'] . ');';
insert($link, $query);

// get updated ranking
$query = 'SELECT scores.user_id, MAX(scores.score) as score, users.name from scores LEFT JOIN users ON scores.user_id = users.id WHERE 1 GROUP BY user_id ORDER BY score DESC;';
$ranking = select($link, $query);

// disconnect
disconnect($link);

echo json_encode($ranking);

