$(document).ready(function () {    
    //---------------------------------------------------------------
   
    //InputValidate(rooms);
    //---------------------------------------------------------------
    function FromValidates()
    {
        $('#myForm').validate({
            rules: {
                Area: {
                    required: true
                }
            },
            messages: {

                Area: {
                    required: "Требуется поле Площадь."
                }
            }
        });
    }
    
    //---------------------------------------------------------------
    $('.menu > ul > li').click(function () {        

        $(".menu > ul").toggleClass('show-on-mobile');

        $('#rezults').empty();        

    });
    //---------------------------------------------------------------


    $('#id_head_price').click(function () {

        var hostname = $(window.location).attr('hostname');

        var url = hostname == "localhost" ? "/Home/FormPrice" : "/duma/Home/FormPrice";

        var body = $('body')[0];

        //$('#main').hide();

        //$(".menu > ul").toggleClass('show-on-mobile');

        $(body).css('background-color', 'gray');

        $('#img_modal').show();

        //$('.container').empty();

        $('.form-load-layout').load(url, function () {

            $('#img_modal').hide();

        });  

        $('#rezults').css('margin-top', 0);

    });
    //---------------------------------------------------------------    
    $('#ref_services_id').click(function () {        

        var hostname = $(window.location).attr('hostname');

        var url = hostname == "localhost" ? "/home/services" : "/duma/home/services";

        var body = $('body')[0];

        //$('#main').hide();

        //$(".menu > ul").toggleClass('show-on-mobile');

        $(body).css('background-color', 'rgb(255, 255, 255)');

        $('#img_modal').show();

        //$('.container').empty();

        $('.form-load-layout').load(url, function () {

            $('#img_modal').hide();

        });

        $('#rezults').css('margin-top', 0);

    });    
    //---------------------------------------------------------------    
    $('#ref_coordinate_id').click(function () {

        var hostname = $(window.location).attr('hostname');

        var url = hostname == "localhost" ? "/home/coordinate" : "/duma/home/coordinate";

        var body = $('body')[0];

        //$('#main').hide();

        //$(".menu > ul").toggleClass('show-on-mobile');

        $(body).css('background-color', 'rgb(255, 255, 255)');

        $('#img_modal').show();

        //$('.container').empty();

        $('.form-load-layout').load(url, function () {

            $('#img_modal').hide();

        });

        $('#rezults').css('margin-top', 0);

    });        
    //---------------------------------------------------------------    

    $(".header-total").click(function () {

        $("#main").show();

        //console.log('zzz');

    });

    //---------------------------------------------------------------

    // // с использованием AJAX запроса загружаем данные с сервера и размещаем, возвращенный HTML код внутри элемента id = rezults, и вызываем функцию обратного вызова
    /*$('.services_href').click(function (event) {

        $.get("/Home/Price", function (data, status) {
            $('#rezults').html(data);
            console.log(status);
        });

        $('#rezults').load("/Home/Price", function (response, status) {
            //console.log(response); // строка соответствующая данным, присланным от сервера
            console.log(status); // строка соответствующая статусу запроса
        });
        
    });    */
    //---------------------------------------------------------------
});