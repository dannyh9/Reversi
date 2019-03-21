let Game = (function () {

    function maakGame(gameNaam, spelerId) {
        console.log("maak nieuw spel aan met naam : " + gameNaam + " Speler id: " + spelerId);

        var data = {
            id: spelerId,
            name: gameNaam
        };

        Ajax.sendPostRequest('api/reversi/game', data,
            function (response) {
                //alert(response);
                PopupWidget.popup("game gemaakt","content van de game");
            },
            function (result, status, xhr) {
                //alert(result.responseText);
            });
    }

    

    function getTurn(gameId) {
        Ajax.sendGetRequest('api/reversi/game/' + gameId +'/turn',
            function (response) {
                var kleur = "";
                console.log(response);
                if (response === 1) {
                    kleur = "wit";
                } else {
                    kleur = "zwart";
                }
                $(".currentturn").text('Nu aan de beurt: ' + kleur);
            },
            function (result, status, xhr) {
                //response json
                console.log(result, status, xhr);
            });
    }

    function getGame(gameId) {
        Ajax.sendGetRequest('api/reversi/game/'+gameId,
            function (response) {
                //console.log(response);
                buildGame(response, gameId);

            },
            function (result, status, xhr) {
                //response json
                //console.log(result, status, xhr);
            });
    }

    function buildGame(gameData) {
        if (gameData !== undefined) {
            
            if (gameData === "DONE") {
                $(".gameFinder").show();
                $(".game").hide();
                $(".gameScreen").html("");
            } else {
                $(".gameFinder").hide();
                var gamehtml = '<table style="margin-top: 20px;" class="rboard" cellspacing="0" cellpadding="0">';
                for (i = 0; i < gameData.length; i++) {
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
        }else  {
            $(".gameScreen").html("");
        }
    }

    function doTurn(location) {
        var locationString = location.toString();
        var x = locationString.charAt(0);
        var y = locationString.charAt(1);
        var gameId = localStorage.getItem('gameid');
        color = localStorage.getItem('color');

        if (color === "0") {
            PopupWidget.popup("FOUT", "selecteer eerst een kleur");
        } else {
            //console.log(x, y, color, gameId);

            var data = {
                X: x,
                Y: y,
                Color: color
            };

            Ajax.sendPostRequest('api/reversi/game/'+gameId, data,
                function (response) {
                    PopupWidget.popup("titel", response);
                },
                function (result, status, xhr) {
                    //alert(result.responseText);
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
