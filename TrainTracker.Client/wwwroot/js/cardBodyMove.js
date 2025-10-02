function moveOnlyCardsBody(){
    document.querySelectorAll('.card-body').forEach(body => {
        body.addEventListener('mousedown', e => {
            body.closest('.card').classList.add('active-card');
        });
        body.addEventListener('mouseup', e => {
            body.closest('.card').classList.remove('active-card');
        });
        body.addEventListener('mouseleave', e => {
            body.closest('.card').classList.remove('active-card');
        });
    });
}
moveOnlyCardsBody();