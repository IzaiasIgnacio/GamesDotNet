$(function() {
    /*========== menu de plataformas ================*/
    //atualizar menu de plataformas e lista de jogos
    $("#MenuPlataforma").on("click",".li_plataforma",function () {
        var element = $(this);
        $.post("/Jquery/MenuPlataformas", { plataforma: element.attr("id") },
        function (resposta) {
            if (element.hasClass("ativo")) {
                element.removeClass("ativo");
            }
            else {
                element.addClass("ativo");
            }
            $("#MenuPlataforma").html(resposta);
            $.post("/Jquery/ListaJogosIndex",
            function (resposta) {
                $("#GameListView").html(resposta);
            });
        });
    });
});