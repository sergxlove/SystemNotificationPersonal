const translations = {
    ru: {
        title: 'Система оповещения',
        server: 'Сервер',
        address: 'Адрес',
        addressPlaceholder: '192.168.1.100:5000',
        save: 'Сохранить',
        credentials: 'Учетные данные',
        login: 'Логин',
        loginPlaceholder: 'Введите логин',
        password: 'Пароль',
        passwordPlaceholder: 'Введите пароль',
        parameters: 'Параметры',
        routeType: 'Тип маршрута (1-5)',
        start: 'Запустить',
        stop: 'Остановить',
        starting: 'Запуск...',
        stopping: 'Остановка...',
        ready: 'Готов к работе',
        active: 'Оповещение активно',
        help: 'ℹ️ Справка',
        helpText: 'URL или IP-адрес сервера для подключения клиента. Определяет куда приложение будет отправлять запросы.',
        examples: 'Примеры:',
        understand: 'Понятно',
        saved: 'Адрес сохранен',
        saveError: 'Ошибка сохранения',
        connectionError: 'Ошибка соединения',
        enterAddress: 'Введите адрес сервера',
        enterCredentials: 'Введите логин и пароль',
        invalidRoute: 'Маршрут: 1-5',
        notificationStarted: 'Оповещение запущено',
        notificationStopped: 'Оповещение остановлено',
        error: 'Ошибка',
        serverError: 'Ошибка сервера'
    },
    en: {
        title: 'Notification System',
        server: 'Server',
        address: 'Address',
        addressPlaceholder: '192.168.1.100:5000',
        save: 'Save',
        credentials: 'Credentials',
        login: 'Login',
        loginPlaceholder: 'Enter login',
        password: 'Password',
        passwordPlaceholder: 'Enter password',
        parameters: 'Parameters',
        routeType: 'Route type (1-5)',
        start: 'Start',
        stop: 'Stop',
        starting: 'Starting...',
        stopping: 'Stopping...',
        ready: 'Ready',
        active: 'Notification active',
        help: 'ℹ️ Help',
        helpText: 'Server URL or IP address for client connection. Determines where the application sends requests.',
        examples: 'Examples:',
        understand: 'Got it',
        saved: 'Address saved',
        saveError: 'Save error',
        connectionError: 'Connection error',
        enterAddress: 'Enter server address',
        enterCredentials: 'Enter login and password',
        invalidRoute: 'Route: 1-5',
        notificationStarted: 'Notification started',
        notificationStopped: 'Notification stopped',
        error: 'Error',
        serverError: 'Server error'
    },
    de: {
        title: 'Benachrichtigungssystem',
        server: 'Server',
        address: 'Adresse',
        addressPlaceholder: '192.168.1.100:5000',
        save: 'Speichern',
        credentials: 'Anmeldedaten',
        login: 'Anmeldung',
        loginPlaceholder: 'Anmeldung eingeben',
        password: 'Passwort',
        passwordPlaceholder: 'Passwort eingeben',
        parameters: 'Parameter',
        routeType: 'Routentyp (1-5)',
        start: 'Starten',
        stop: 'Stoppen',
        starting: 'Startet...',
        stopping: 'Stoppt...',
        ready: 'Bereit',
        active: 'Benachrichtigung aktiv',
        help: 'ℹ️ Hilfe',
        helpText: 'Server-URL oder IP-Adresse für Client-Verbindung. Bestimmt, wohin die Anwendung Anfragen sendet.',
        examples: 'Beispiele:',
        understand: 'Verstanden',
        saved: 'Adresse gespeichert',
        saveError: 'Speicherfehler',
        connectionError: 'Verbindungsfehler',
        enterAddress: 'Serveradresse eingeben',
        enterCredentials: 'Anmeldung und Passwort eingeben',
        invalidRoute: 'Route: 1-5',
        notificationStarted: 'Benachrichtigung gestartet',
        notificationStopped: 'Benachrichtigung gestoppt',
        error: 'Fehler',
        serverError: 'Serverfehler'
    },
    fr: {
        title: 'Système de notification',
        server: 'Serveur',
        address: 'Adresse',
        addressPlaceholder: '192.168.1.100:5000',
        save: 'Enregistrer',
        credentials: 'Identifiants',
        login: 'Identifiant',
        loginPlaceholder: 'Entrer identifiant',
        password: 'Mot de passe',
        passwordPlaceholder: 'Entrer mot de passe',
        parameters: 'Paramètres',
        routeType: 'Type de route (1-5)',
        start: 'Démarrer',
        stop: 'Arrêter',
        starting: 'Démarrage...',
        stopping: 'Arrêt...',
        ready: 'Prêt',
        active: 'Notification active',
        help: 'ℹ️ Aide',
        helpText: 'URL ou adresse IP du serveur pour la connexion client. Détermine où l\'application envoie les requêtes.',
        examples: 'Exemples:',
        understand: 'Compris',
        saved: 'Adresse enregistrée',
        saveError: 'Erreur d\'enregistrement',
        connectionError: 'Erreur de connexion',
        enterAddress: 'Entrer l\'adresse du serveur',
        enterCredentials: 'Entrer identifiant et mot de passe',
        invalidRoute: 'Route: 1-5',
        notificationStarted: 'Notification démarrée',
        notificationStopped: 'Notification arrêtée',
        error: 'Erreur',
        serverError: 'Erreur serveur'
    }
};

const flags = { ru: '🇷🇺', en: '🇬🇧', de: '🇩🇪', fr: '🇫🇷' };
const langNames = { ru: 'Русский', en: 'English', de: 'Deutsch', fr: 'Français' };
const state = {
    isNotify: false,
    serverAddress: '',
    theme: localStorage.getItem('theme') || 'light',
    language: localStorage.getItem('language') || 'ru',
    loading: false
};
const $ = id => document.getElementById(id);
const elements = {
    serverAddress: $('serverAddress'),
    login: $('login'),
    password: $('password'),
    routeType: $('routeType'),
    toggleButton: $('toggleButton'),
    btnIcon: $('btnIcon'),
    btnText: $('btnText'),
    statusDot: $('statusDot'),
    statusText: $('statusText'),
    toastContainer: $('toastContainer'),
    helpModal: $('helpModal'),
    langDropdown: $('langDropdown'),
    currentLangFlag: $('currentLangFlag'),
    currentLangName: $('currentLangName')
};
document.addEventListener('DOMContentLoaded', async () => {
    applyTheme(state.theme);
    applyLanguage(state.language);
    await loadSettings();

    document.addEventListener('click', (e) => {
        if (!e.target.closest('.lang-selector')) {
            elements.langDropdown.classList.remove('show');
        }
    });
});
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
}
function applyLanguage(lang) {
    document.documentElement.lang = lang;

    document.querySelectorAll('[data-i18n]').forEach(el => {
        const key = el.getAttribute('data-i18n');
        if (translations[lang]?.[key]) el.textContent = translations[lang][key];
    });

    document.querySelectorAll('[data-i18n-placeholder]').forEach(el => {
        const key = el.getAttribute('data-i18n-placeholder');
        if (translations[lang]?.[key]) el.placeholder = translations[lang][key];
    });

    elements.btnText.textContent = translations[lang][state.isNotify ? 'stop' : 'start'];
    elements.statusText.textContent = translations[lang][state.isNotify ? 'active' : 'ready'];
    elements.currentLangFlag.textContent = flags[lang];
    elements.currentLangName.textContent = langNames[lang];
}
async function loadSettings() {
    const saved = localStorage.getItem('serverAddress');
    if (saved) {
        state.serverAddress = saved;
        elements.serverAddress.value = saved;
    }
}
function saveServerAddress() {
    const addr = elements.serverAddress.value.trim();
    const lang = state.language;

    if (!addr) {
        showToast(translations[lang].enterAddress, 'warning');
        return;
    }

    state.serverAddress = addr;
    localStorage.setItem('serverAddress', addr);
    showToast(translations[lang].saved, 'success');
}
function showHelp() { elements.helpModal.style.display = 'flex'; }
function closeHelpModal() { elements.helpModal.style.display = 'none'; }
async function toggleNotification() {
    if (state.loading) return;
    state.isNotify ? await stopNotification() : await startNotification();
}
async function startNotification() {
    const login = elements.login.value.trim();
    const password = elements.password.value.trim();
    const addressServer = elements.serverAddress.value.trim();
    const variableExit = parseInt(elements.routeType.value);
    const lang = state.language;
    if (!login || !password) {
        showToast(translations[lang].enterCredentials, 'warning');
        return;
    }
    if (!addressServer) {
        showToast(translations[lang].enterAddress, 'warning');
        return;
    }
    if (variableExit < 1 || variableExit > 5) {
        showToast(translations[lang].invalidRoute, 'warning');
        return;
    }
    setLoading(true, 'starting');
    try {
        const res = await fetch('/notify/start', {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify({ login, password, addressServer, variableExit })
        });

        if (res.ok) {
            const msg = await res.text();
            showToast(msg || translations[lang].notificationStarted, 'success');
            state.isNotify = true;
            updateUI(true);
        } else {
            const error = await res.text();
            showToast(`${translations[lang].error}: ${error || res.status}`, 'error');
        }
    } catch (e) {
        showToast(`${translations[lang].connectionError}: ${e.message}`, 'error');
    } finally {
        setLoading(false);
    }
}
async function stopNotification() {
    const addressServer = elements.serverAddress.value.trim();
    const lang = state.language;

    if (!addressServer) {
        showToast(translations[lang].enterAddress, 'warning');
        return;
    }
    setLoading(true, 'stopping');
    try {
        const res = await fetch('/notify/stop', {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify({ addressServer })
        });

        if (res.ok) {
            const msg = await res.text();
            showToast(msg || translations[lang].notificationStopped, 'success');
            state.isNotify = false;
            updateUI(false);
        } else {
            const error = await res.text();
            showToast(`${translations[lang].error}: ${error || res.status}`, 'error');
        }
    } catch (e) {
        showToast(`${translations[lang].connectionError}: ${e.message}`, 'error');
    } finally {
        setLoading(false);
    }
}
function updateUI(isActive) {
    const lang = state.language;
    elements.toggleButton.className = `btn btn-large ${isActive ? 'btn-stop' : 'btn-primary'}`;
    elements.btnIcon.textContent = isActive ? '⏹️' : '▶️';
    elements.btnText.textContent = translations[lang][isActive ? 'stop' : 'start'];
    elements.statusDot.classList.toggle('inactive', !isActive);
    elements.statusText.textContent = translations[lang][isActive ? 'active' : 'ready'];
}
function setLoading(loading, action = 'starting') {
    const lang = state.language;
    state.loading = loading;
    elements.toggleButton.disabled = loading;
    if (loading) {
        elements.btnIcon.innerHTML = '<span class="spinner"></span>';
        elements.btnText.textContent = translations[lang][action];
    } else {
        elements.btnIcon.textContent = state.isNotify ? '⏹️' : '▶️';
        elements.btnText.textContent = translations[lang][state.isNotify ? 'stop' : 'start'];
    }
}
function showToast(msg, type = 'info') {
    const toast = document.createElement('div');
    toast.className = `toast toast-${type}`;
    const icons = { success: '✅', error: '❌', warning: '⚠️', info: 'ℹ️' };
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
window.saveServerAddress = saveServerAddress;
window.showHelp = showHelp;
window.closeHelpModal = closeHelpModal;
window.toggleNotification = toggleNotification;