let connection = null;
setupConnection = () => {
    connection = new signalR.HubConnectionBuilder()
        .withUrl("/counthub")
        .build();
    //一个ReceiveUpdate监听方法，接受一个参数
    connection.on("ReceiveUpdate", (update) => {
        const resultDiv = document.getElementById("result");
        //更新div内容
        resultDiv.innerHTML = update;
    });
    connection.on("someFunc", (obj) => {
        const resultDiv = document.getElementById("result");
        resultDiv.innerHTML = "SomeOne called ,paras:" + obj.random;
    });
    connection.on("Finished", function () {
        connection.stop();
        const resultDiv = document.getElementById("result");
        resultDiv.innerHTML = "Finished";
    });
    connection.start().catch(err => console.error(err.toString()));
}
setupConnection();

//为提交按钮绑定click事件
document.getElementById("submit").addEventListener("click", e => {
    e.preventDefault();
    fetch("/api/count",
        {
            method: "POST",
            headers: { 'content-type': 'application/json' }
        })
        .then(response => response.text())
        .then(id => connection.invoke("GetLatestCount", id));
});