$(() => {
    loadPeople();

    function loadPeople() {
        $.get('/home/getall', function (people) {
            $('#people-table tr:gt(0)').remove();
            people.forEach(person => {
                $('tbody').append(`
                    <tr>
                        <td>${person.firstName}</td>
                        <td>${person.lastName}</td>
                        <td>${person.age}</td>
                        <td>
                            <button class="btn btn-secondary btn-block edit" data-id="${person.id}" 
                                    data-first-name="${person.firstName}" data-last-name="${person.lastName}" data-age="${person.age}">Edit</button>
                        </td>
                        <td>
                            <button class="btn btn-secondary btn-block delete" data-id=${person.id}>Delete</button>
                        </td>
                    </tr>`);
            });
        });
    }

    $('#add-person').on('click', function () {
        const firstName = $('#first-name').val()
        const lastName = $('#last-name').val()
        const age = $('#age').val()

        $.post('/home/addperson', { firstName, lastName, age }, function (person) {
            loadPeople();
            $('#first-name').val('');
            $('#last-name').val('');
            $('#age').val('');
        });
    });

    $('#people-table').on('click', '.edit', function () {
        const button = $(this);
        const id = button.data('id');
        let firstName = button.data('first-name');
        let lastName = button.data('last-name');
        let age = button.data('age');

        $('#edit-first-name').val(firstName);
        $('#edit-last-name').val(lastName);
        $('#edit-age').val(age);
        $('#modal-title').append(firstName + ' ' + lastName);
        $('.modal').modal();

        $('#save').on('click', function () {
            console.log('clicked');
            firstName = $('#edit-first-name').val();
            lastName = $('#edit-last-name').val();
            age = $('#edit-age').val();
            $('.modal').modal('hide');

            $.post('/home/editperson', { firstName, lastName, age, id }, function (person) {
                loadPeople();
            });
        });        
    });

    $('#people-table').on('click', '.delete', function () {
        const button = $(this);
        const id = button.data('id');
        $.post('/home/deleteperson', { id }, function (id) {
            loadPeople();
        });
    });
});