$(function() {
    $("#busca").keyup(function () {
        $.post("/Jquery/BuscarJogoEntityJquery", { search: $(this).val() },
        function (resposta) {
            $("#lista_resultados_busca").html(resposta);
            $("#lista_resultados_busca").slideDown();
        });
    });
});