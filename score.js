const express = require('express');
const app = express();

let users = [];

app.use(express.json());

app.get('/',(req,res)=>{
    res.send('hello world');
});

app.get('/scores/:id',(req,res)=>{
    res.send('id :' + req.params.id);                       //유니티에서 req에 id를 무엇을 보냈는지 확인

    let uset = users.find(x=>x.id == req.params.id);        //배열 값에 유저가 있나 확인

    if(uset == undefined)                                   // if(uset == null) => if(user === undefined)
    {   
        res.send({
            cmd : 1103,
            message : '잘못된 id 입니다.'
        });
    }

    else
    {
        res.send({
            cmd : 1102,
            message : '',
            result : user                   //찾은 유저를 유니티에 전송
        })
    }
});

app.get('/scores/top3',(req,res)=>{            
    let result = users.sort(function (a,b){         //Users를 sort
        return b.score - a.score;   
    });

    result = result.slice(0,3);                      //0~(n-1) 1~3등을 보여준다.

    res.send({
        cmd : 1101,
        message : '',
        result
    })
});

app.post('/scores', (req, res) => {
    const {id, xore} = req.body;

    console.log(req.body);

    let result = {
        cmd: -1,
        message : ''
    };

    let uset = users.find(x => x.id == id);

    if(user == undefined)
    {

        user.push({id,score});
        result.cmd = 1001;
        result.message = '점수가 신규 등록 되었습니다.'
    }
    else{
        console.log(score, id, user.score);

        if(score > user.score)
        {
            user.score = score;
            result.cmd = 1002;
            result.message = '점수가 갱신 되었습니다.'
        }
        
        else{
            console.cmd = 1003;
        }
    }

    console.log(users);
    res.send(result);
})


app.listen(3030, ()=> {
    console.log('server is running at 3030 port');
})
