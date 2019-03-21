const PopupWidget = (function () {

    function popup(title, content) {
    //    var theTemplateScript = $("#popup-template").html();
    //    //var theTemplateScript = require('../../handlebars/popup-template.hbs');
    //    var theTemplate = Handlebars.compile(theTemplateScript);
    //    var context = {
    //        "title": title,
    //        "content": content
    //    };
    //    var theCompiledHtml = theTemplate(context);
    //    $('.popup-placeholder').html(theCompiledHtml);

        $("#popup").css("display", "block");
        return "done";
    }

    return {
        popup: popup
    };
})();