$(function () {
    $("#add-person").on('click', function () {
        $("#update-person").hide();
        $("#save-person").show();
        $("#firstName").val('');
        $("#lastName").val('');
        $("#age").val('');
        $(".modal").modal();
    });

    function getPeople() {
        $.get('/home/getall', function (people) {
            $("table tr:gt(0)").remove(); //clear table
            people.forEach(function (person) {
                $("table").append("<tr><td>" + person.FirstName + "</td><td>" + person.LastName +
                    "</td><td>" + person.Age + "</td><td><button class='btn btn-warning edit' data-person-id='" + person.Id + "'>Edit</button>" +
                    "<button class='btn btn-danger delete' data-person-id='" + person.Id + "'>Delete</button>");
            });
        });
    }

    $("#save-person").on('click', function () {
        var firstName = $("#firstName").val();
        var lastName = $("#lastName").val();
        var age = $("#age").val();
        $.post("/home/addperson", { firstName: firstName, lastName: lastName, age: age }, function () {
            getPeople();
            $(".modal").modal('hide');
        });
    });

    $("table").on('click', '.delete', function () {
        var personId = $(this).data('person-id');
        if (!confirm("Are you sure?")) {
            return;
        }

        $.post("/home/deleteperson", { id: personId }, function (result) {
            getPeople();
        });
    });

    $("table").on('click', '.edit', function() {
        var personId = $(this).data('person-id');
        $.get("/home/getperson", { id: personId }, function(person) {
            $("#firstName").val(person.FirstName);
            $("#lastName").val(person.LastName);
            $("#age").val(person.Age);
            $("#update-person").show();
            $("#save-person").hide();
            $("#update-person").data('person-id', personId);
            $(".modal").modal();
        });
    });

    $("#update-person").on('click', function() {
        var firstName = $("#firstName").val();
        var lastName = $("#lastName").val();
        var age = $("#age").val();
        var personId = $(this).data('person-id');
        $.post("/home/updateperson", { firstName: firstName, lastName: lastName, age: age, id: personId }, function() {
            getPeople();
            $(".modal").modal('hide');
        });
    });
});