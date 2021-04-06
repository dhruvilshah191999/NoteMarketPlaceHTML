$(document).ready(function () {
  $("#mobile-nav-open-btn").click(function () {
    $("#mobile-nav").css("height", "100%");
  });

  $("#mobile-nav-close-btn").click(function () {
    $("#mobile-nav").css("height", "0%");
  });
});

$(document).ready(function () {
  document.getElementById("file_value").onchange = function () {
    document.getElementById("upload_picture").innerHTML = this.value;
  };

  document.getElementById("file_value2").onchange = function () {
    document.getElementById("upload_picture2").innerHTML = this.value;
  };
});

$(document).ready(function () {
    $("#Memail").css("min-height", $("#MemailValue").height());
    $("#Maddress1").css("min-height", $("#Maddress1Value").height());
    $("#Maddress2").css("min-height", $("#Maddress2Value").height());
});
