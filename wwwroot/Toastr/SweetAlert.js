function showAlert(type,message) {
    Swal.fire({
        title: type.toUpperCase(),
        text: message,
        icon: type,
        confirmButtonText: 'OK'
    });
}

function showConfirm() {
    Swal.fire({
        title: 'Are you sure?',
        text: "You won't be able to revert this action!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, do it!'
    }).then((result) => {
        if (result.isConfirmed) {
            Swal.fire(
                'Done!',
                'Your action has been confirmed.',
                'success'
            )
        }
    })
}





