$(document).ready(function () {
    $("#mobile-nav-open-btn").click(function () {
        $("#mobile-nav").css("height", "100%");
    });

    $("#mobile-nav-close-btn , #mobile-nav a").click(function () {
        $("#mobile-nav").css("height", "0%");
    });
});

$(document).ready(function () {
    $(".faq-second-border").hide();
    for (let i = 1; i <= 8; i++) {
        $("#faq-" + i).click(function () {
            $("#faq-" + i).hide(500);
            $("#faq-part-" + i).slideToggle(500);
        });

        $("#faq-part-" + i).click(function () {
            $("#faq-part-" + i).slideUp(500, () => {
                $("#faq-" + i).show(500);
            });
        });
    }
});

$(document).ready(function () {
    document.getElementById("file_value").onchange = function () {
        document.getElementById("upload_picture").innerHTML = this.value;
    };
});

$(document).ready(function () {
    document.getElementById("file_value1").onchange = function () {
        document.getElementById("upload_note_picture").innerHTML = this.value;
    };

    document.getElementById("file_value2").onchange = function () {
        document.getElementById("upload_note").innerHTML = this.value;
    };

    document.getElementById("file_value3").onchange = function () {
        document.getElementById("upload_note_preview").innerHTML = this.value;
    };
});

$(document).ready(function () {
    $("#institution").css("min-height", $("#institutionvalue").height());
    $("#course").css("min-height", $("#coursevalue").height());
    $("#professor").css("min-height", $("#professorvalue").height());
});
