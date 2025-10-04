function playSound() {
    const audio = new Audio('/sound/notification.wav');
    audio.volume = 0.1;       
    audio.currentTime = 0;    
    audio.play().catch(err => console.log('Ошибка воспроизведения:', err));
}