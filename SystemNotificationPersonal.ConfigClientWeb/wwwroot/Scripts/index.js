const translations = {
    ru: {
        title: 'Конфигурация клиента',
        serverAddress: 'Адрес сервера',
        exePath: 'Путь к исполняемому файлу',
        theme: 'Тема приложения',
        header: 'Заголовок формы оповещения',
        timeout: 'Время до выключения (сек)',
        current: 'Текущее значение: ',
        apply: 'Применить',
        show: 'Показать конфигурацию',
        reset: 'Сброс',
        start: 'Запустить',
        startAdmin: 'Запустить с правами администратора',
        ready: 'Готов к работе',
        currentConfig: 'Текущая конфигурация',
        close: 'Закрыть',
        saved: 'Конфигурация успешно обновлена',
        resetSuccess: 'Конфигурация успешно сброшена',
        appStarted: 'Фоновая задача запущена',
        appStartedAdmin: 'Фоновая задача запущена с правами администратора',
        error: 'Ошибка',
        serverHelp: 'URL или IP-адрес сервера для подключения клиента.\nОпределяет куда приложение будет отправлять запросы.',
        exeHelp: 'Полный путь к EXE-файлу главного приложения.\nОпределяет какое приложение будет запускаться у пользователя.',
        themeHelp: 'Визуальное оформление интерфейса.\nВлияет на внешний вид всех элементов управления.',
        headerHelp: 'Текст в заголовке окна главного приложения.\nОтображается в верху главного приложения.',
        timeoutHelp: 'Таймер обратного отсчета перед автоматическим завершением работы системы.\nУказывается в секундах.'
    },
    en: {
        title: 'Client Configuration',
        serverAddress: 'Server Address',
        exePath: 'Executable Path',
        theme: 'Application Theme',
        header: 'Notification Form Header',
        timeout: 'Shutdown Timeout (sec)',
        current: 'Current value: ',
        apply: 'Apply',
        show: 'Show Configuration',
        reset: 'Reset',
        start: 'Start',
        startAdmin: 'Start as Administrator',
        ready: 'Ready',
        currentConfig: 'Current Configuration',
        close: 'Close',
        saved: 'Configuration successfully updated',
        resetSuccess: 'Configuration successfully reset',
        appStarted: 'Background task started',
        appStartedAdmin: 'Background task started with administrator privileges',
        error: 'Error',
        serverHelp: 'Server URL or IP address for client connection.',
        exeHelp: 'Full path to the main application EXE file.',
        themeHelp: 'Visual interface design.',
        headerHelp: 'Text in the title of the main application window.',
        timeoutHelp: 'Countdown timer before automatic system shutdown.'
    },
    de: {
        title: 'Client-Konfiguration',
        serverAddress: 'Serveradresse',
        exePath: 'Pfad zur EXE',
        theme: 'Anwendungsdesign',
        header: 'Benachrichtigungsformular-Header',
        timeout: 'Abschaltzeit (Sek)',
        current: 'Aktueller Wert: ',
        apply: 'Anwenden',
        show: 'Konfiguration anzeigen',
        reset: 'Zurücksetzen',
        start: 'Starten',
        startAdmin: 'Als Administrator starten',
        ready: 'Bereit',
        currentConfig: 'Aktuelle Konfiguration',
        close: 'Schließen',
        saved: 'Konfiguration erfolgreich aktualisiert',
        resetSuccess: 'Konfiguration erfolgreich zurückgesetzt',
        appStarted: 'Hintergrundaufgabe gestartet',
        appStartedAdmin: 'Hintergrundaufgabe mit Administratorrechten gestartet',
        error: 'Fehler'
    },
    fr: {
        title: 'Configuration Client',
        serverAddress: 'Adresse du serveur',
        exePath: 'Chemin de l\'exécutable',
        theme: 'Thème de l\'application',
        header: 'En-tête du formulaire',
        timeout: 'Délai d\'arrêt (sec)',
        current: 'Valeur actuelle: ',
        apply: 'Appliquer',
        show: 'Afficher la configuration',
        reset: 'Réinitialiser',
        start: 'Démarrer',
        startAdmin: 'Démarrer en tant qu\'administrateur',
        ready: 'Prêt',
        currentConfig: 'Configuration actuelle',
        close: 'Fermer',
        saved: 'Configuration mise à jour',
        resetSuccess: 'Configuration réinitialisée',
        appStarted: 'Tâche d\'arrière-plan démarrée',
        appStartedAdmin: 'Tâche d\'arrière-plan démarrée avec privilèges administrateur',
        error: 'Erreur'
    }
};
const flags = { ru: '🇷🇺', en: '🇬🇧', de: '🇩🇪', fr: '🇫🇷' };
const langNames = { ru: 'Русский', en: 'English', de: 'Deutsch', fr: 'Français' };
const state = {
    theme: localStorage.getItem('theme') || 'light',
    language: localStorage.getItem('language') || 'ru',
    config: {}
};
const elements = {
    serverAddress: document.getElementById('serverAddress'),
    exePath: document.getElementById('exePath'),
    theme: document.getElementById('theme'),
    headerText: document.getElementById('headerText'),
    timeout: document.getElementById('timeout'),
    currentServerAddress: document.getElementById('currentServerAddress'),
    currentExePath: document.getElementById('currentExePath'),
    currentTheme: document.getElementById('currentTheme'),
    currentHeader: document.getElementById('currentHeader'),
    currentTimeout: document.getElementById('currentTimeout'),
    langDropdown: document.getElementById('langDropdown'),
    currentLangFlag: document.getElementById('currentLangFlag'),
    currentLangName: document.getElementById('currentLangName'),
    configModal: document.getElementById('configModal'),
    configContent: document.getElementById('configContent'),
    toastContainer: document.getElementById('toastContainer')
};
document.addEventListener('DOMContentLoaded', async () => {
    applyTheme(state.theme);
    applyLanguage(state.language);
    await loadConfig();

    document.addEventListener('click', (e) => {
        if (!e.target.closest('.lang-selector')) {
            elements.langDropdown.classList.remove('show');
        }
    });
});
async function loadConfig() {
    try {
        const res = await fetch('/api/config');
        if (res.ok) {
            state.config = await res.json();
            updateCurrentValues();
        }
    } catch (e) {
        console.error('Ошибка загрузки конфигурации:', e);
    }
}
function updateCurrentValues() {
    elements.currentServerAddress.textContent = (translations[state.language].current || 'Текущее значение: ') + (state.config.addressServer || '');
    elements.currentExePath.textContent = (translations[state.language].current || 'Текущее значение: ') + (state.config.pathExe || '');
    elements.currentTheme.textContent = (translations[state.language].current || 'Текущее значение: ') + (state.config.theme || '');
    elements.currentHeader.textContent = (translations[state.language].current || 'Текущее значение: ') + (state.config.header || '');
    elements.currentTimeout.textContent = (translations[state.language].current || 'Текущее значение: ') + (state.config.timeBeforeOffPC || '');
}
async function applyConfig() {
    const config = {
        addressServer: elements.serverAddress.value,
        pathExe: elements.exePath.value,
        theme: elements.theme.value,
        header: elements.headerText.value,
        timeBeforeOffPC: parseInt(elements.timeout.value) || 0
    };
    try {
        const res = await fetch('/api/config', {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(config)
        });
        if (res.ok) {
            state.config = config;
            updateCurrentValues();
            showToast(translations[state.language].saved, 'success');
        } else {
            showToast(translations[state.language].error, 'error');
        }
    } catch (e) {
        showToast(`${translations[state.language].error}: ${e.message}`, 'error');
    }
}
function showConfig() {
    elements.configContent.textContent = JSON.stringify(state.config, null, 2);
    elements.configModal.style.display = 'flex';
}
function closeModal() {
    elements.configModal.style.display = 'none';
}
async function resetConfig() {
    try {
        const res = await fetch('/api/config/reset', { method: 'POST' });
        if (res.ok) {
            await loadConfig();
            showToast(translations[state.language].resetSuccess, 'success');
        } else {
            showToast(translations[state.language].error, 'error');
        }
    } catch (e) {
        showToast(`${translations[state.language].error}: ${e.message}`, 'error');
    }
}
async function startApp() {
    try {
        const res = await fetch('/api/app/start', { method: 'POST' });
        if (res.ok) {
            showToast(translations[state.language].appStarted, 'success');
        } else {
            showToast(translations[state.language].error, 'error');
        }
    } catch (e) {
        showToast(`${translations[state.language].error}: ${e.message}`, 'error');
    }
}
async function startAppAsAdmin() {
    try {
        const res = await fetch('/api/app/start-admin', { method: 'POST' });
        if (res.ok) {
            showToast(translations[state.language].appStartedAdmin, 'success');
        } else {
            showToast(translations[state.language].error, 'error');
        }
    } catch (e) {
        showToast(`${translations[state.language].error}: ${e.message}`, 'error');
    }
}
function toggleTheme() {
    state.theme = state.theme === 'light' ? 'dark' : 'light';
    applyTheme(state.theme);
    localStorage.setItem('theme', state.theme);
}
function applyTheme(theme) {
    document.body.setAttribute('data-theme', theme);
    document.querySelector('.theme-icon').textContent = theme === 'light' ? '🌙' : '☀️';
}
function toggleLangDropdown() {
    elements.langDropdown.classList.toggle('show');
}
function setLanguage(lang) {
    state.language = lang;
    applyLanguage(lang);
    localStorage.setItem('language', lang);
    elements.langDropdown.classList.remove('show');
    elements.currentLangFlag.textContent = flags[lang];
    elements.currentLangName.textContent = langNames[lang];
    updateCurrentValues();
}
function applyLanguage(lang) {
    document.documentElement.lang = lang;
    document.querySelectorAll('[data-i18n]').forEach(el => {
        const key = el.getAttribute('data-i18n');
        if (translations[lang]?.[key]) {
            el.textContent = translations[lang][key];
        }
    });
    document.querySelectorAll('[data-i18n-tooltip]').forEach(el => {
        const key = el.getAttribute('data-i18n-tooltip');
        if (translations[lang]?.[key]) {
            el.setAttribute('data-tooltip', translations[lang][key]);
        }
    });
    elements.currentLangFlag.textContent = flags[lang];
    elements.currentLangName.textContent = langNames[lang];
}
function showToast(msg, type = 'info') {
    const toast = document.createElement('div');
    toast.className = `toast toast-${type}`;
    const icons = { success: '✅', error: '❌', info: 'ℹ️' };
    toast.innerHTML = `<span>${icons[type]}</span><span>${msg}</span>`;
    elements.toastContainer.appendChild(toast);
    setTimeout(() => {
        toast.style.animation = 'slideIn 0.3s reverse';
        setTimeout(() => toast.remove(), 300);
    }, 4000);
}
window.toggleTheme = toggleTheme;
window.toggleLangDropdown = toggleLangDropdown;
window.setLanguage = setLanguage;
window.applyConfig = applyConfig;
window.showConfig = showConfig;
window.closeModal = closeModal;
window.resetConfig = resetConfig;
window.startApp = startApp;
window.startAppAsAdmin = startAppAsAdmin;