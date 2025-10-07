function closeModal(modalName){
    let modalEl = document.getElementById(modalName);
    let modal = bootstrap.Modal.getInstance(modalEl);
    if (!modal) modal = new bootstrap.Modal(modalEl);
    modal.hide();
}