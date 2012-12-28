$(document).ready(function () {

  $('.modal').modalResponsiveFix({ debug: false });

  //feedback button clicked
  $("#feedback_add").bind("click", function () {

    $('#modal-from-dom-feedback').modal('hide');

    var fdata = { 'feedbackmessage': $("#feedbackmessage").val(), 'url': window.location.href };

    $('#feedbackmessage').val('');

    $.ajax({
      url: "/home/sendfeedback",
      type: "POST",
      contentType: "application/x-www-form-urlencoded",
      data: fdata,
      success: function (r) {
        if (r.status != '201') {
          $("#result").html("<div class='icon-warning-sign'></div> ERROR " + r.status + " " + r.message).addClass("alert alert-error");
        } else {
          $("#result").html("<div class='icon-ok icon-white'></div> SUCCESS <a href='" + r.html_url + "'>" + r.html_url + "</a>").addClass("alert");
        }
      },
      error: function (xhr, ajaxOptions, thrownError) {
        $("#result").html("<div class='icon-warning-sign'></div> ERROR " + xhr.responseText).addClass("alert alert-error");
      }
    });

    return false;
  });

  //when modal opens focus on textbox
  $('#modal-from-dom-feedback').on('shown', function () {
    $('#feedbackmessage').focus().val('');
  });
});