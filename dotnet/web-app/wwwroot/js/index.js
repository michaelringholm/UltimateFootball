var apiRootUrl = {};
$(function() {
    apiRootUrl = $("#api-root-url").val();
    console.log("JQuery initialized!");

    /*
    <button id="btnHome" type="button" class="btn btn-primary" style="margin-top: 1rem;">Home</button>
    <button id="btnLeague" type="button" class="btn btn-primary" style="margin-top: 1rem;">League</button>
    <button id="btnSquad" type="button" class="btn btn-primary" style="margin-top: 1rem;">Squad</button>
    <button id="btnStadion" type="button" class="btn btn-primary" style="margin-top: 1rem;">Stadion</button>
    <button id="btnEconomy" type="button" class="btn btn-primary" style="margin-top: 1rem;">Economy</button>
    <button id="btnTransfers" type="button" class="btn btn-primary" style="margin-top: 1rem;">Transfers</button>
    */

    var home = new Home();
    var league = new League();
    var squad = new Squad();
    var stadion = new Stadion();
    var economy = new Economy();
    var transfers = new Transfers();
    home.focus();
});

function Home() {
    var _this = this;
    
    var construct = function() {    
        $("#btnHome").click(function() {_this.focus();});
    };

    this.showManagerDetails = function() {
        console.log("showManagerDetails clicked!");
        var managerId = "fe32f338-123b-5836-a868-77a885a9b0d3";
        $.ajax({
            type: "POST",
            contentType: "application/json",
            url: "/api/manager/show-details",
            data: JSON.stringify(managerId)
          }).done(function(manager, status) {
            console.log("showing league table...");
            $("#managerName").html(manager.name);
          });

        /*$.post("/api/manager/show-details", JSON.stringify(managerId), function(manager, status) {
            console.log("showing league table...");
            $("#managerName").html(manager.name);
        });*/
    };    

    this.focus = function() {
        $(".fdSection").hide();
        $("#home").show();
        _this.showManagerDetails();
    };

    construct();
}

function Transfers() {
    var _this = this;
    
    var construct = function() {    
        $("#btnTransfers").click(function() {_this.focus();});
    };

    this.focus = function() {
        $(".fdSection").hide();
        $("#transfers").show();
    };

    construct();
}

function Economy() {
    var _this = this;
    
    var construct = function() {    
        $("#btnEconomy").click(function() {_this.focus();});
    };

    this.focus = function() {
        $(".fdSection").hide();
        $("#economy").show();
    };

    construct();
}

function Squad() {
    var _this = this;
    
    var construct = function() {    
        $("#btnSquad").click(function() {_this.focus();});
    };

    this.focus = function() {
        $(".fdSection").hide();
        $("#squad").show();
    };

    construct();
}

function Stadion() {
    var _this = this;
    
    var construct = function() {    
        $("#btnStadion").click(function() {_this.focus();});
    };

    this.focus = function() {
        $(".fdSection").hide();
        $("#stadion").show();
    };

    construct();
}

function League() {
    var _this = this;
    
    var construct = function() {
        $("#btnLeague").click(function() {_this.focus();});
    };

    this.showLeagueTable = function() {
        console.log("showLeagueTable clicked!");
        var leagueId = "0e69f338-456a-4736-a868-80a997a9b0c7";
        var data = { leagueId: leagueId };
        $.post("/api/league/show-league-table", function(data, status) {
            console.log("showing league table...");
            var source = $("#leagueTableTemplate").html();
            var compiledTemplate = Handlebars.compile(source);
            $("#leagueTableBlade").html(compiledTemplate(data));
        });
    };

    this.focus = function() {
        $(".fdSection").hide();
        $("#league").show();        
        _this.showLeagueTable();
    };

    construct();
}

console.log("JavaScript initialized!");