// função que envia a tabela que deve ser estilizada pelo plugin DataTable e com a configuração para português 
$(document).ready(function () {
    getDatatable('#tableContacts');
    getDatatable('#tableUsers');
    
    $('.btn-total-contatos').click(function () {
        var usuarioId = $(this).attr('usuario-id');
        
        $.ajax({
            type: 'GET',
            url: '/Usuario/ListContactsById/' + usuarioId, 
            success: function(result){
               $("#ListContactsUser").html(result);
               $('#modalContatosUsuario').modal('show');
                getDatatable('#tableContactsUser');
            }
        });
        
    });
});

// função que estiliza a tabela e configura o plugin DataTable em português
function getDatatable( id ) {
    $(id).DataTable({
        "ordering": true,
        "paging": true,
        "searching": true,
        "oLanguage": {
            "sEmptyTable": "Nenhum registro encontrado na tabela",
            "sInfo": "Mostrar _START_ até _END_ de _TOTAL_ registros",
            "sInfoEmpty": "Mostrar 0 até 0 de 0 Registros",
            "sInfoFiltered": "(Filtrar de _MAX_ total registros)",
            "sInfoPostFix": "",
            "sInfoThousands": ".",
            "sLengthMenu": "Mostrar _MENU_ registros por pagina",
            "sLoadingRecords": "Carregando...",
            "sProcessing": "Processando...",
            "sZeroRecords": "Nenhum registro encontrado",
            "sSearch": "Pesquisar",
            "oPaginate": {
                "sNext": "Proximo",
                "sPrevious": "Anterior",
                "sFirst": "Primeiro",
                "sLast": "Ultimo"
            },
            "oAria": {
                "sSortAscending": ": Ordenar colunas de forma ascendente",
                "sSortDescending": ": Ordenar colunas de forma descendente"
            }
        }
    });
}

$('.close-alert').click(function () {
    $('.alert').hide('hide');
});

function showPassword(inputId, buttonId) {
    var inputPassword = document.getElementById(inputId);
    var btnShow = document.getElementById(buttonId);
    
    if ( inputPassword.type === 'password' ) {
        inputPassword.setAttribute('type', 'text');
        btnShow.classList.replace('bi-eye', 'bi-eye-slash');
    } else {
        inputPassword.setAttribute('type', 'password');
        btnShow.classList.replace('bi-eye-slash', 'bi-eye');
    }
}