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
  for (let i = 1; i <= 7; i++) {
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
