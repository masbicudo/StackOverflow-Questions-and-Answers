$(function() {
    $('#btn').click(function() {
            alert("clicou");
            $.ajax({
                url: 'http://jsfiddle.net/echo/html/',
                data: {
                    html: '<div>Documento carregado via ajax!</div>',
                    delay: 1
                },
                type: 'POST',
                success: function(data){
                    alert("sucesso");
                    var win = window.open(
                        "",
                        "Title",
                        "toolbar=no,"+
                        "location=no,"+
                        "directories=no,"+
                        "status=no,"+
                        "menubar=no,"+
                        "scrollbars=yes,"+
                        "resizable=yes,"+
                        "width=780,"+
                        "height=200,"+
                        "top="+(screen.height-400)+","+
                        "left="+(screen.width-840));
                    win.document.body.innerHTML = data;
                },
                error: function() {
                    alert("error: "+JSON.stringify(arguments));
                }
            });
            return false;
        });
});
