//Navbar Script
$(document).ready(function() {
    $(".collapse-inner").each(function() {
        if (!$(this).has("a").length) {
            $(this).closest("li").addClass("hidden-menu");
        }
    });

    //$("#logoutSubmit").on("click",
    //    function() {
    //        $("#logoutForm").submit();
    //    });
});