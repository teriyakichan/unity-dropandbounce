<?php
require('./func.php');

// db connect
$link = connect();

// select
$query = 'SELECT scores.user_id, MAX(scores.score) as score, users.name from scores LEFT JOIN users ON scores.user_id = users.id WHERE 1 GROUP BY user_id ORDER BY score DESC;';

$ranking = select($link, $query);
// disconnect
disconnect($link);

header('Access-Control-Allow-Origin: *');
header('Access-Control-Allow-Headers: Origin, X-Requested-With, Content-Type, Accept');
echo json_encode($ranking);

