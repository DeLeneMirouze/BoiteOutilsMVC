
$(document).ready(function ()
{
    $(".edit").live("click", function (e)
    {
        e.preventDefault();

        // id de la personne à éditer
        var personneId = $(this).attr("id").replace('edit-', '');

        $.ajax(
        {
            type: "GET",
            url: "Popin/EditPersonne",
            data: { id: personneId },
            cache: false,
            dataType: "json",

            success: function (myPersonView)
            {
                // => alimente le contenu de la popin
                $("#edit-popup").html(myPersonView.Html);
                // ouvre la popin
                // l'aspect est mieux si on charge la css: themes/base/jquery-ui.css
                // ce que l'on fait dans le layout
                $("div#edit-popup").dialog();
            } 
        }); 
    }); 
});