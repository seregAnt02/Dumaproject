$(document).ready(function () {
    
    //----------------------------------------------------
    //----------------------------------------------------

    $('.menu > ul > li:has( > ul)').addClass('menu-dropdown-icon');

    $('.menu > ul > li > ul:not(:has(ul))').addClass('normal-sub');

    $(".menu > ul").before('<span class="menu-mobile">Меню:</span>');
    

    $(".menu-mobile").click(function (e) {


        $(".menu > ul").toggleClass('show-on-mobile');

        var main_titul = $('#main')[0].scrollHeight + 9.8;

        $('#main').toggle();

        var height_menu_mobil = $(".menu > ul")[0].scrollHeight;        
        

        var offset_result = height_menu_mobil == 0 ? 0 : -(height_menu_mobil - main_titul);                

        //console.dir(offset_result);        

        if (height_menu_mobil == 0) {

            $('#rezults').css('margin-top', 0);
        }
        else {            

            $('#rezults').css('margin-top', offset_result ).css('display', 'block');  //-212.4               
            
        }        
        
    });
    //----------------------------------------------------    
    $('.menu > ul > li').css('border-radius', '10px');

    $('.menu-container').css('border-radius', '10px');    
    //----------------------------------------------------
});    
