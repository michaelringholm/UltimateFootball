$(function() {
    console.log("JQuery initialized!");
    var management = new Management();
    $("#checkWebAPIHealth").click(function() {
        management.CheckAPIHealth();
    });
});

function Management() {
    var _this = this;

    this.CheckAPIHealth = function() {        
        console.log("checkWebAPIHealth clicked!");
        $.get("https://com-dev-demo-api.azurewebsites.net/api/Management/health", function(data, status){
            console.log(data);
        });
    };
}

console.log("JavaScript initialized!");