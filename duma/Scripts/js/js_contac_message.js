$(document).ready(function () {

    //---------------------------------------------

    // вешаем маску на телефон
    $('.phone-field').inputmask("+7(999)999-9999");

    // добавляем правило для валидации телефона
    jQuery.validator.addMethod("checkMaskPhone", function (value, element) {

        return /\+\d{1}\(\d{3}\)\d{3}-\d{4}/g.test(value);

    });

    //---------------------------------------------

    // вешаем валидацию на поле с телефоном по классу
    $.validator.addClassRules({

        'phone-field': {

            checkMaskPhone: true

        }
    });
    //---------------------------------------------

    var forma = $('.form-contact').validate({

        //Введите номер телефона

        rules: {
            name: {

                required: true,
                minlength: 2,
                maxlength: 21

            },

            telephone: {

                required: true

            },

            emailadress: {

                required: true

            }

        },
        messages: {

            name: {

                required: "Введите Имя.",
                minlength: "Ведите более 2-х знаков.",
                maxlength: "Ведите не более 21-и знаков"

            },

            telephone: {

                required: "Введите номер телефона."

            },

            emailadress: {

                required: "Введите email почту."

            }

        }

    });

    //---------------------------------------------

    var check_box_privacy = $('.check_box-privacy-policy')[0];    

    //---------------------------------------------

    $('#id_telephone').on("keyup change click", function () {
        
        $('span.field-validation-error').remove();        

        if ($(this).val().length > 0) {

            $('#formButton').attr('disabled', null);

            $(this).css('background-color', 'rgb(255, 255, 255)');            
        }        

    });

    //---------------------------------------------

    $('#id_name').on("keyup change click", function () {

        var val = $(this).val();

        //$('span.field-validation-error').empty();
        $('span.field-validation-error').remove();

        if (val.length > 1 && val.length < 22) {

            $('#formButton').attr('disabled', null);

            $(this).css('background-color', 'rgb(255, 255, 255)');
            //console.log(phone.valid());
        }        

    });


    //---------------------------------------------


    $('#id_emailadress').on("change click", function () {

        var email = $('#id_emailadress').val();

        //$('span.field-validation-error').empty();
        $('span.field-validation-error').remove();

        if (IsEmail(email)) {

            $(this).css('background-color', 'rgb(255, 255, 255)');

            $('#formButton').attr('disabled', null);

        }        

    });


    //---------------------------------------------

    function IsEmail(email) {

        var regex = /^([a-zA-Z0-9_\.\-\+])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/;

        if (!regex.test(email)) {

            return false;

        }
        else {

            return true;

        }
    }

    //---------------------------------------------    
    //---------------------------------------------

    $('#formButton').click(function () {

        var hostname = $(window.location).attr('hostname');

        var url = hostname == "localhost" ? "/Home/ContactMessage" : "/duma/Home/ContactMessage";        

        if ($('.form-contact').valid() && check_box_privacy.checked) {


            with_table();

            var dataObj = $(':input');

            var content = $(".content-contact-message")[0];

            var formdata = dataObj.serialize();

            //var json_data = '{"model":{"name":"' + name + '", "telephone":"' + telephone + '", "emailadress":"' + emailadress + '"}, "content":"' + content.textContent + '"}';                        

            $(this).attr('disabled', true);

            var body = $('body')[0];

            $(body).css('background-color', 'gray');

            var img_modal = $('#img_modal')[0];

            $('.form-contact').css('opacity', '10%');

            $(img_modal).show();

            //console.log(formdata + '&content=' + content.textContent + '&urlupload=' + url_upload);

            $.ajax({
                type: "POST",
                url: url,
                data: formdata + '&content=' + content.textContent + '&urlupload=' + url_upload,
                success: function (data) {

                    $(body).css('background-color', 'rgb(255, 255, 255)');

                    $('#img_modal').hide();

                    $('body').html(data);
                    //console.log(data);

                }
            });

        }
        else {                                    

            $('#formButton').attr('disabled', 'disabled');

            //$('.check_box-privacy-policy').after('<span class="field-validation-error">Для продолжения введите корректные данные.</span>').css('background-color', '#F08080');                        
        }

    });
    //---------------------------------------------    
    function Upload() {

        var hostname = $(window.location).attr('hostname');

        var url = hostname == "localhost" ? "/Home/Upload" : "/duma/Home/Upload";

        var path = (window.URL || window.webkitURL).createObjectURL(event.target.files[0]);

        var files = document.getElementById('download_file').files;            

        //files[0].name = path + ".txt";

        //console.log(files);

        if (files.length) {

            var data = new FormData();

            var expansion = null;

            if (files[0].type == "application/vnd.openxmlformats-officedocument.wordprocessingml.document") {

                expansion = ".docx";

            }

            if (files[0].type == "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet") {

                expansion = ".xlsx";

            }

            if (files[0].type == "application/vnd.openxmlformats-officedocument.presentationml.presentation") {

                expansion = ".pptx";

            }

            if (files[0].type == "application/vnd.ms-publisher") {

                expansion = ".pub";

            }

            if (expansion != null) {

                for (var x = 0; x < files.length; x++) {

                    data.append("file" + x, files[x], path + expansion);
                }

                return $.ajax({
                    type: "POST",
                    url: url,
                    contentType: false,
                    processData: false,
                    data: data
                });

            }                        
            
        }        
    }        
    //---------------------------------------------
    function with_table() {
        var width = $(window).width();
        var tex_box = $('.text-box-wishes');
        if (width > 400) {

            tex_box.width('710px');
            
        }
        else {

            tex_box.width('290px');

            $("#img_modal").css("margin-top", "-600px")

        }
    }
    //---------------------------------------------
    with_table();
    //---------------------------------------------

    var url_upload;

    $('#download_file').change(function (event) {        

        //var filePath = $(this).val();

        //var filePath = $('#id_img').attr('src', path);

        event.preventDefault();

        url_upload = null;

        Upload().done(function (url_data) {

            url_upload = url_data;

            //console.log(url_data);

        });                

    });
        

    //---------------------------------------------

    $('.ref-a-check-box-privacypolicy').click(function () {

        var hostname = $(window.location).attr('hostname');

        var url = hostname == "localhost" ? "/Policy/PrivacyPolicy" : "/duma/Policy/PrivacyPolicy";

        var body = $('body')[0];

        //$(body).css('background-color', 'gray');

        $('#rezults').css("display", "none");

        var img_modal = $('#img_modal')[0];

        $(img_modal).show().css("margin-top", "200px");   

        $('.form-load-layout').load(url, function () {

            //$(body).css('background-color', 'bisque');                    

            $(img_modal).hide();

        });

    });

    $('.ref-a-check-box-soglasie').click(function () {

        var hostname = $(window.location).attr('hostname');

        var url = hostname == "localhost" ? "/Policy/Soglasie" : "/duma/Policy/Soglasie";

        var body = $('body')[0];

        //$(body).css('background-color', 'gray');

        $('#rezults').css("display", "none");

        var img_modal = $('#img_modal')[0];

        $(img_modal).show().css("margin-top", "200px");

        $('.form-load-layout').load(url, function () {

            //$(body).css('background-color', 'bisque');

            $(img_modal).hide();

        });

    });
    //---------------------------------------------

    let check_box_soglasie = document.getElementsByClassName("check_box-soglasie");

    $(".check_box-soglasie").click(function () {

        $('span.field-validation-error').remove();

        if ($(this)[0].checked && $('.field-validation-error').length == 0 && $('.check_box-privacy-policy')[0].checked) {


            $('#formButton').attr('disabled', null);

        }
        else $('#formButton').attr('disabled', 'disabled');    

        //console.log(check_box_soglasie[0].checked);
    });    

    //---------------------------------------------
    

    $('.check_box-privacy-policy').click(function () {                
        
        $('span.field-validation-error').remove();

        if ($(this)[0].checked && $('.field-validation-error').length == 0 && check_box_soglasie[0].checked) {
            

            $('#formButton').attr('disabled', null);            
            
        }
        else $('#formButton').attr('disabled', 'disabled');        

    });

    //---------------------------------------------

});