$(document).ready(function () {
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
        Game.getGame(gameid);
        setInterval(function () {
            Game.getGame(gameid);
        }, 1000);
    });
    $('.rsquare').on("click", function () {
        
        
    });
    $(".white").click(function () {
        localStorage.removeItem('color');
        localStorage.setItem('color', 1);
        $(".currentcolor").html('huidige kleur: wit');
    });
    $(".black").click(function () {
        localStorage.removeItem('color');
        localStorage.setItem('color', 2);
        $(".currentcolor").html('huidige kleur: zwart');
    });
    $(document.body).on('click', '.rsquare', function () {
        var location = $(this).data("location");
        Game.doTurn(location);
    });
});
