/* swiper slide side-bar */
var swiper = new Swiper(".slide-swp", {
    pagination: {
      el: ".swiper-pagination",
      dynmicBullests: true,
      clickable: true
    },
    autoplay:{
      delay:2500,
    },
    loop: true,
  });


  /* swiper slide Sale_slide */
var swiper = new Swiper(".sale_sec", {

    slidesPerView:5,
    spaceBetween:30,
    autoplay:{
      delay:3000,
    },
    navigation:{
        nextEl:".swiper-button-next",
        prevEl:".swiper-button-prev"
    },
    loop: true,
  });



  var swiper = new Swiper(".product_swip", {

    slidesPerView:4,
    spaceBetween:30,
    autoplay:{
      delay:3000,
    },
    navigation:{
        nextEl:".swiper-button-next",
        prevEl:".swiper-button-prev"
    },
    loop: true,
  });