//open & close cart

var cart = document.querySelector('.cart');
function open_cart(){
    cart.classList.add("active")
}

function close_cart(){
    cart.classList.remove("active")
}






   
   let bigImge = document.getElementById("bigImg");


   function ChangeItemImage(img){
    
    bigImge.src = img

   }