$(document).ready(function () {
    //----------------------------------------------------       
    //----------------------------------------------------
    $('.compose').click(function () {

        $('.overlay').addClass('is-open');

        $('body').css('background', 'rgba(0, 0, 0, 0.7)');

    });


    $('.modal-close, .send').click(function () {

        $('.overlay').removeClass('is-open');

        $('body').css('background', '#D4DBE2');

    });
    //----------------------------------------------------
    //var modal = $('.modal_mod');

    $('.modal_mod').css('margin-top', -90).css('width', 600);    

    //----------------------------------------------------
    var button_send = $('.modal-button');
    var model = $(':input');
    var table_param = $('.table_param tr > td');
    model[3].value = table_param[1].textContent;// parameter
    model[4].value = table_param[2].textContent;// codparameter
    model[5].value = table_param[3].textContent;// meaning
    model[7].value = table_param[4].textContent;// lastupdate
    $(button_send).click(function () {
        var formdata = model.serialize();
        //отключение стандартной отправки формы
        event.preventDefault();        
        var posting = $.post('', formdata, function (data, status) {
            //console.log(posting);                                    
            $(window.location).attr("href", "http://localhost:8888/parameter/" + table_param[0].textContent); //http переход  к консольному приложеню
        });              
    });
    //----------------------------------------------------
    $(window).resize(function () {
        var width = $(window).width();            
        if (width > 120) {
            //$('.table_param').css('width', 800);
            //$('.table_param').css('font-size', '14px');
        }
        //if (width > 1000) $('.table_param').css('width', 800);
    });
    //----------------------------------------------------
    //$('.table_param').css('font-size', '7px');
    //$('.table_param').css('width', '120px');
    //console.log($('.table_param').css('font-size'));
    //----------------------------------------------------
    // ѕрослушка событи€ смены ориентации
    window.addEventListener("orientationchange", function () {
        // ¬ыводим числовое значение ориентации        
        //if (window.orientation >= 0) $('.table_param').css('width', 300);
        //if (window.orientation < 0) $('.table_param').css('width', 600);
    }, false);
    //----------------------------------------------------
    $('#main').css('display', 'none');
});