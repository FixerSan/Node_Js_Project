
//HTTP 모듈 불러오기
var http = require("http");

//HTTP 서버 8000포트를 여는 함수
http.createServer(function (require, response){

    //Content Type 정의 선언
    response.writeHead(200, {'Content - Type' : 'text/plain'});

    //리스펀스를 Hello World 로 한다.
    response.end('hello world\n');

}).listen(8000);

console.log('Server running at http://127.0.0.1:8000/')