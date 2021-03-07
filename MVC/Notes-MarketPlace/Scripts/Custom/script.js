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
  showHideNav();
  $(window).scroll(function () {
    showHideNav();
  });

  function showHideNav() {
    if ($(window).scrollTop() > 55) {
      $("nav").addClass("white-nav-top");
      $(".navbar-brand img").attr("src", "./images/login/logo.png");
    } else {
      $("nav").removeClass("white-nav-top");
      $(".navbar-brand img").attr("src", "./images/login/top-logo.png");
    }
  }
});

$(document).ready(function () {
  $("#mobile-nav-open-btn").click(function () {
    $("#mobile-nav").css("height", "80%");
  });

  $("#mobile-nav-close-btn , #mobile-nav a").click(function () {
    $("#mobile-nav").css("height", "0%");
  });
});
