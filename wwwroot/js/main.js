$(document).ready(function () {

    $(document.body).on('click', '.close', function () {
        $('#popup').css("display", "none");
    });

    $('.spinner').hide();
    $("#newGameButton").click(function () {
        var gameName = $("#newGame").val();
        var spelerId = $("#spelerId").val();
        Game.maakGame(gameName, spelerId);
    });

    $(".joinGameButton").click(function () {
        var gameid = $(this).data("id");
        localStorage.setItem('gameid', gameid);
        localStorage.removeItem('color');
        localStorage.setItem('color', 0);
        $(".gameFinder").hide();
        $('.spinner').show();
        setTimeout(function () {
            $('.spinner').hide();
            Game.getGame(gameid);
            Game.getTurn(gameid);
            Broodje.broodjeKlaarmaken();
            Api.getImages();
            //PopupWidget.popup("game opgestart", "content van de game");
            setInterval(function () {
                Game.getGame(gameid);
                Game.getTurn(gameid);
            }, 1000);
        }, 3000);
        
    });

    $(".white").click(function () {
        localStorage.removeItem('color');
        localStorage.setItem('color', 1);
        $(".currentcolor").html('huidige kleur: wit');
    });
    $(".black").click(function () {
        localStorage.removeItem('color');
        localStorage.setItem('color', 2);
        $(".currentcolor").html('Geselecteerde kleur: zwart');
    });

    $(document.body).on('click', '.rsquare', function () {
        var location = $(this).data("location");
        Game.doTurn(location);
    });
});
