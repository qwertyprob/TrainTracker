function pulseBackground(parrent){
    const allCardsBody = document.querySelectorAll(parrent);

    allCardsBody.forEach(card => {

            if (card.querySelector('.delayed')){
                card.classList.add('pulse-card');
            }
        }

    )
}

function pulseTable(parentSelector){
    document.querySelectorAll(parentSelector).forEach(row => {
        if (row.querySelector('.delayed')) {
            row.classList.add('pulse-card');
        }
    });
}

pulseBackground('.card-body');
