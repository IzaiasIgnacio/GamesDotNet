$(function() {
    /*========== modal dados jogo ================*/
    //exibir inclusao de jogo
    $(".navbar-brand").click(function() {
        $("#modal_game").modal('show');
    });
    //incluir linha de plataforma
    $("#btn_add").click(function () {
        $.post("/Jquery/AdicionarPlataformaJquery",
        function (resposta) {
            $(".span_plataformas").append(resposta);
        });
    });
    //remover linha de plataforma
    $(".fieldset_plataformas").on("click", ".remover_plataforma", function () {
        $(this).closest(".plataforma_status").fadeOut('normal', function () {
            $(this).remove();
        });
    });
    //buscar jogos no igdb por titulo
    $(".btn_buscar_igdb").click(function () {
        $.post("/Jquery/BuscarJogoJquery", { search: $("#Titulo").val() },
        function (resposta) {
            $("#lista_resultados").html(resposta);
            $("#lista_resultados").slideDown();
        });
    });
    //carregar formulario com dados do igdb
    $("#lista_resultados").on("click", ".div_resultados .row", function () {
        $.post("/Jquery/PreencherDadosGameIgdbJquery", { id_igdb: $(this).find(".id_igdb").val() },
        function (resposta) {
            $("#DadosGameView").html(resposta);
            $("#modal_game").modal('show');
            $("#lista_resultados").fadeOut();
        });
    });
    //salvar novo jogo
    /*$("#DadosGameView").on("click",".salvar",function () {
        $.post("/Jquery/SalvarNovoJogoJquery", { valores: $("#form_game").serialize() },
        function (resposta) {
            $("#DadosGameView").html(resposta);
            $("#modal_game").modal('show');
            $("#lista_resultados").fadeOut();
        });
    });*/

    /*========== grid de jogos ================*/
    //exibir modal para alterar jogo
    $(".thumbnail").click(function () {
        //$("#modal_game").modal('show');
    });

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