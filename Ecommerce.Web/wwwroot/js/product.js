$(() => {

    $("#addtocart").on('click', function () {
        const pId = $("#productid").val();
        const quant = $("#quantity").val();
        const sId = $("#shoppingid").val();
        $.post('/home/addproducttocart', { productId: pId, quantity: quant, cartId: sId }, function (b) {
            if (b) {
                $("#addedtocart").modal('show');
            }
            else {
                $("#title").text("Product is Already in Cart");
                $("#viewcart").text("Change Quantity in Cart");
                $("#addedtocart").modal('show');
            }
        });
    });


});