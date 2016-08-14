
$("#images").on('change', function () {

    var countFiles = $(this)[0].files.length;

    var imgPath = $(this)[0].value;
    var extn = imgPath.substring(imgPath.lastIndexOf('.') + 1).toLowerCase();
    var image_holder = $("#image-holder");
    image_holder.empty();

    var filesize = document.getElementById('images').files[0].size;

    var images = $("#images");

    if (filesize > 200000) {
        alert("Za duzy obrazek. Dopuszczalna wielkość do 200KB");
        images.replaceWith(images = images.clone(true));
        return;
    }

    if (extn == "gif" || extn == "png" || extn == "jpg" || extn == "jpeg") {
        if (typeof (FileReader) != "undefined") {

            //loop for each file selected for uploaded.
            for (var i = 0; i < countFiles; i++) {

                var filesize = document.getElementById('images').files[i].size;

                if (filesize > 200000) {
                    alert("Za duzy obrazek. Dopuszczalna wielkość do 200KB");
                    images.replaceWith(images = images.clone(true));
                    return;
                }

                var reader = new FileReader();
                reader.onload = function (e) {
                    $("<img />", {
                        "src": e.target.result,
                        "class": "img-rounded",
                        "style": "max-width:200px; margin:10px;"
                    }).appendTo(image_holder);

                }

                image_holder.show();
                reader.readAsDataURL($(this)[0].files[i]);
            }

        } else {
            alert("Ta przeglądarka nie obsługuje FileReader.");
        }
    } else {
        alert("Dodany plik nie jest obrazkiem");
    }

});