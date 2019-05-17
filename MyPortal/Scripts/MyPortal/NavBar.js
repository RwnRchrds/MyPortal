//Navbar Script
$(document).ready(function() {
        $(document).ready(function() {
            $(".nav-app-menu").each(function() {
                if (!$(this).has("a").length) {
                    $(this).parent().addClass("hidden-menu");
                }
            });          
        });
});