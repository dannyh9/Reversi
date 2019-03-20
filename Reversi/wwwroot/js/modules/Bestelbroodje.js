let Broodje = (function () {

function bestelBroodje(naam) {
    return new Promise(function (resolve, reject) {
        //object litteral
        let bestelling = { naam: naam, prijs: 0 };

        resolve(bestelling);
    });
}

function broodjeKlaarmaken(bestelling) {
    return new Promise(function (resolve, reject) {
        //bestelBroodje('kaas broodje')
        setTimeout(resolve(bestelling), 2000);

    });
}

function bestellingAfrekenen(bestelling) {
    return new Promise(function (resolve, reject) {
        PopupWidget.popup(bestelling.naam, "bestelling geplaatst");
        if (bestelling.naam === "kaas broodje") {
            bestelling.prijs = 1.80;
            PopupWidget.popup(bestelling.naam, "bestelling geplaatst");
            resolve(bestelling);
        }
        if (bestelling.naam === "kroket broodje") {
            bestelling.prijs = 2.50;
            PopupWidget.popup(bestelling.naam, "bestelling geplaatst");
            resolve(bestelling);
        } else {
            reject(bestelling);
        }


    }).catch(function (error) {
        console.log(`error: ${error}`);
    });
}

bestelBroodje("kroket broodje").then(function (value) {
    return broodjeKlaarmaken(value);
}).then(function (value) {
    return bestellingAfrekenen(value);
        });


    return {
        broodjeKlaarmaken: broodjeKlaarmaken
    };



})();