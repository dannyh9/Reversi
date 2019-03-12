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
            Game.getTurn(gameid);
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

let Ajax = (function () {
    function sendGetRequest(url, onSuccess, onError) {
        return sendRequest(url, null, 'GET', onSuccess, onError);
    }

    function sendPatchRequest(url, data, onSuccess, onError) {
        return sendRequest(url, data, 'PATCH', onSuccess, onError);
    }

    function sendPutRequest(url, data, onSuccess, onError) {
        return sendRequest(url, JSON.stringify(data), 'PUT', onSuccess, onError);
    }

    function sendPostRequest(url, data, onSuccess, onError) {
        return sendRequest(url, JSON.stringify(data), 'POST', onSuccess, onError);
    }

    function sendDeleteRequest(url, onSuccess, onError) {
        return sendRequest(url, null, 'DELETE', onSuccess, onError);
    }

    function sendRequest(url, data, verb, onSuccess, onError) {
        var request = {
            url: url,
            type: verb,
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',
            data: data,
            success: onSuccess,
            error: onError
        };
        return $.ajax(request);
    }

    return {
        sendPostRequest: sendPostRequest,
        sendGetRequest: sendGetRequest
    };
})();

let Game = (function () {

    function maakGame(gameNaam, spelerId) {
        console.log("maak nieuw spel aan met naam : " + gameNaam + " Speler id: " + spelerId);

        var data = {
            id: spelerId,
            name: gameNaam
        };

        Ajax.sendPostRequest('api/reversi/game', data,
            function (response) {
                alert(response);
            },
            function (result, status, xhr) {
                alert(result.responseText);
            });
    }
    function getTurn(gameId) {
        Ajax.sendGetRequest('api/reversi/game/' + gameId +'/turn',
            function (response) {
                console.log(response);
            },
            function (result, status, xhr) {
                //response json
                console.log(result, status, xhr);
            });
    }

    function getGame(gameId) {
        Ajax.sendGetRequest('api/reversi/game/'+gameId,
            function (response) {
                console.log(response);
                //buildGame(response, gameId);

            },
            function (result, status, xhr) {
                //response json
                console.log(result, status, xhr);
            });
    }

    function buildGame(gameData, gameId) {
        if (gameData === "DONE") {
            $(".gameFinder").show();
            $(".game").hide();
        } else {
            $(".gameFinder").hide();
            var gamehtml = '<table style="margin-top: 20px;" class="rboard" cellspacing="0" cellpadding="0">';
            for (i = 0; i < gameData.length; i++){
                gamehtml += '<tr>';
                for (ii = 0; ii < gameData[i].length; ii++) {
                    gamehtml += '<td class="rsquare" data-location="' + i + ii + '" >';
                    if (gameData[i][ii] === 1) {
                        gamehtml += "<img src='images/white.gif'>";
                    } else if (gameData[i][ii] === 2) {
                        gamehtml += "<img src='images/black.gif'>";
                    } else {
                        gamehtml += "<img src='images/green.gif'>";
                    }
                    gamehtml += '</td>';
                    
                }
                gamehtml += '</tr>';
            }
            gamehtml += '</table>';
            $(".gameScreen").html(gamehtml);
            $(".game").show();
        }
        
    }

    function doTurn(location) {
        var locationString = location.toString();
        var x = locationString.charAt(0);
        var y = locationString.charAt(1);
        var gameId = localStorage.getItem('gameid');
        color = localStorage.getItem('color');

        if (color === "0") {
            alert("selecteer een kleur");
        } else {
            //console.log(x, y, color, gameId);

            var data = {
                X: x,
                Y: y,
                Color: color,
            };

            Ajax.sendPostRequest('api/reversi/game/'+gameId, data,
                function (response) {
                    alert(response);
                },
                function (result, status, xhr) {
                    alert(result.responseText);
                });
        }
    }


    return {
        maakGame: maakGame,
        getGame: getGame,
        buildGame: buildGame,
        doTurn: doTurn,
        getTurn: getTurn
    };
})();
