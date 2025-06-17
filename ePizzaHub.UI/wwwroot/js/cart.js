//function AddToCart(ItemId, UnitPrice, Quantity) {
//    $.ajax({
//        type: "POST",
//        url: "/Cart/AddToCart/" + ItemId + "/" + UnitPrice + "/" + Quantity,
//        success: function (res) {
//            $('#cartCounter').text(res.Count)
//        },
//        error: function (res) {
//            alert("something is wrong");
//        }
//    })
//}

function AddToCart(ItemId, UnitPrice, Quantity) {
    $.ajax({
        type: "GET",
        "url": "/Cart/AddToCart/" + ItemId + "/" + UnitPrice + "/" + Quantity,
        success: function (res) {
            $("#cartCounter").text(res.count);
        },
        error: function (res) {
            alert("Something went wrong!!");
        }
    })
}



function updateQuantity(id, currentQuantity, quantity) {
    if (parseInt(currentQuantity) >= 1 && parseInt(quantity)>-1) {
        $.ajax({
            type: "PUT",
            contentType: "application/json; charset=utf-8",
            "url": "/Cart/UpdateQuantity/" + id + "/" + quantity,
            success: function (res) {
                $("#cartCounter").text(res.count);
            },
            error: function (res) {
                console.log(res)
            }
        });
    }
}