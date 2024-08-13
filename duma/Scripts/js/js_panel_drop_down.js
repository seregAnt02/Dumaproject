$(document).ready(function () {
    //----------------------------------------
    //----------------------------------------
    var side_panel = $('.side-panel');
    var side_button1 = $('.side-button-1 .side-b');
    var sum_int = null;
    //----------------------------------------
    $(window).resize(function () {
        //with_table();
        //var margin_left = $('.container').css('margin-left');
        //var padding = $('.container').css('padding-right');
        sum_int = Margin_left();//Number.parseInt(padding) + Number.parseInt(margin_left);
        //$('.side-button-1 .side-b').css('margin-left', sum_int);
        $('.side-panel').css('margin-left', sum_int);        
        //console.log(margin_left);
    });
    //----------------------------------------
    function with_table()
    {
        var width = $(window).width();
        if (width > 400) {
            //$('body').css('background-color','yellow');
            $('.side-panel').css('width', '320');
            $('.table_param').css('font-size', '14px');
            $('.table_param').css('width', 800);
            $('.table_param').css('border-color', 'blue');
        }
        else {
            //$('body').css('background-color','green');
            $('.side-panel').css('width', '220');
            $('.table_param').css('font-size', '9px');
            $('.table_param').css('width', 350);
            $('.table_param').css('border-color', 'red');
        }
    }
    with_table();
    //----------------------------------------
    function Margin_left() {                

        var mas_margin_l = $('.container').css('margin-left').split('px');

        var int_padding = null; if (mas_margin_l.length > 0) {

            int_padding = Number.parseFloat(mas_margin_l[0]);            

        }

        var mas_padding = $('.container').css('padding-right').split('px');

        var int_margin_left = null; if (mas_padding.length > 0) {

            int_margin_left = Number.parseFloat(mas_padding[0]);            

        }                        
        return int_margin_left + int_padding;        
    }    
    //----------------------------------------
    /*при нажатий на ссылку, выравнивает копку по левой границы контейнера
    $('.menu_href').click(function (event) {
        var sum_int = Margin_left();
        $('.side-button-1 .side-b').css('margin-left', sum_int);
        $('.side-panel').css('margin-left', sum_int);
    });*/
    //----------------------------------------
    sum_int = Margin_left();

    //----------------------------------------
    //var width_panel = $('.side-panel').css('width');
    //----------------------------------------
    //$('.side-button-1 .side-b').css('margin-left', sum_int);
    $('.side-panel').css('margin-left', sum_int);
    //console.log(sum_int);
    //$('.side-panel').css('background-color', 'red');    
    //----------------------------------------
    $(document).mousedown(function (eventData) {
        //var off_set_X = eventData.;
        var margin_left = $('.container').css('margin-left');        
        var mas_side_panel = side_panel.css('margin-left').split('px');
        var margin_left_panel = null; if (mas_side_panel.length > 0) {
            margin_left_panel = Number.parseFloat(mas_side_panel[0]);
            //console.log(mas_side_panel[0]);
        }        

        var width_panel = Number.parseFloat(side_panel[0].offsetWidth) + margin_left_panel;
        //var top_panel = side_panel[0].offsetTop;
        //var height_panel = side_panel[0].offsetHeight;
        if (eventData.clientX > width_panel &&
            eventData.clientY > 205)//eventData.offsetX > width_panel
        {
            $(side_panel).css('left', '-750px');
            $('.side-b').text('close');
        }        
        //console.log(margin_left_panel);
        //console.log($(document));
    });    
    //----------------------------------------
    $('.side-button-1 .side-b').click(function () {
        var mas = $('.side-panel').css('left').split('px');                
        if (mas.length > 0 && Number.parseInt(mas[0]) > -1)
        {
            $(side_panel).css('left', '-750px');
            $('.side-b').text('close');
        }            
        else
        {
            $(side_panel).css('left', '0px');
            $('.side-b').text('open');
        }        
    });    
    //----------------------------------------
    $('.side-panel .side-button-2').click(function () {
        var mas = $('.side-panel').css('left').split('px');
        if (mas.length > 0 && Number.parseInt(mas[0]) > -1)
        {
            $(side_panel).css('left', '-750px');
            $('.side-b').text('close');
        }            
        else
        {
            $(side_panel).css('left', '0px');
            $('.side-b').text('open');
        }            
    });
    //----------------------------------------
    $('.side-b').css('text-align', 'center');
    //----------------------------------------
    //$('#browser > li > ul a').css('color', 'red');
    //----------------------------------------
});