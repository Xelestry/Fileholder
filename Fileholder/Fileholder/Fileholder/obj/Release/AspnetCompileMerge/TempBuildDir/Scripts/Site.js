

$(document).ready(function () {
    
    $(".main_input_file").change(function () {
        var counter = 0;
        var f_name = [];
        for (var i = 0; i < $(this).get(0).files.length; ++i) {
            ++counter;
        }
        if (counter > 0) {
            var labelText = "Количество выбранных файлов: " + counter;
            $("#f_name").val(f_name = "Количество выбранных файлов: " + counter);
            $("#Change-first-hidden-text").text(labelText);
            $("#Change-second-hidden-text").text(labelText);
            $("#Change-third-hidden-text").text("Нажмите \"Добавить\" для загрузки файлов");
            $("#change-disabled").prop('disabled', false).removeClass("btn-outline-danger").addClass("btn-outline-success");
        } else
        {
            $("#change-disabled").prop('disabled', true).removeClass("btn-outline-success").addClass("btn-outline-danger");
            $("#Change-first-hidden-text").text("Наведите мышку");
            $("#Change-second-hidden-text").text("Нажмите, для выбора файлов");
            $("#Change-third-hidden-text").text("");
        }            
        counter = 0;
    });
});
