$(document).ready(function () {
  window.addEventListener(
    "load",
    function () {
      var forms = document.getElementsByClassName("needs-validation");
      var validation = Array.prototype.filter.call(forms, function (form) {
        form.addEventListener(
          "submit",
          function (event) {
            if (form.checkValidity() === false) {
              event.preventDefault();
              event.stopPropagation();
            }
            form.classList.add("was-validated");
          },
          false
        );
      });
    },
    false
  );
});

$(document).ready(function () {
  $(".toggle-password").click(function () {
    $(this).toggleClass("fa-eye fa-eye-slash");
    var input = $($(this).attr("toggle"));
    if (input.attr("type") == "password") {
      input.attr("type", "text");
    } else {
      input.attr("type", "password");
    }
  });
});

$(document).ready(function () {
  $("#mobile-nav-open-btn").click(function () {
    $("#mobile-nav").css("height", "80%");
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