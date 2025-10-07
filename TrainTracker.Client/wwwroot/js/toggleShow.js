function toggleShow() {
    const tableBtn = document.getElementById('tableBtn');
    const cardsBtn = document.getElementById('cardsBtn');

    const tableRow = document.getElementById('tableRow');
    const cardsRow = document.getElementById('cardsRow');

    const tableTxt = document.getElementById('tableTxt');
    const cardsTxt = document.getElementById('cardsTxt');

    function showTable() {
        tableRow.classList.add('visible');
        tableRow.classList.remove('hidden');

        cardsRow.classList.add('hidden');
        cardsRow.classList.remove('visible');

        tableTxt.classList.remove('hidden');
        cardsTxt.classList.add('hidden');

        tableBtn.classList.add('active');
        cardsBtn.classList.remove('active');

        localStorage.setItem('trainView', 'table');
    }

    function showCards() {
        cardsRow.classList.add('visible');
        cardsRow.classList.remove('hidden');

        tableRow.classList.add('hidden');
        tableRow.classList.remove('visible');

        cardsTxt.classList.remove('hidden');
        tableTxt.classList.add('hidden');

        cardsBtn.classList.add('active');
        tableBtn.classList.remove('active');

        localStorage.setItem('trainView', 'cards');
    }

    tableBtn.addEventListener('click', showTable);
    cardsBtn.addEventListener('click', showCards);

    const savedView = localStorage.getItem('trainView');
    if (savedView === 'cards') {
        showCards();
    } else {
        showTable();
    }
    
}

document.addEventListener('DOMContentLoaded', () => {
    toggleShow();
});
