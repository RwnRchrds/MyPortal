$(document).ready(function() {

    // Clear form inside modal
    $(".modal").on('hidden.bs.modal',
        function () {
            clearModal($(this));
        });
});

function clearModal(modal) {
    modal.find("form")[0].reset();
}