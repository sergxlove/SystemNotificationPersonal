const translations = {
    ru: {
        title: 'Конфигурация сервера',
        tabConfig: 'Определение переменных',
        tabProfiles: 'Управление профилями',
        port: 'Порт сервера',
        dbPath: 'Путь к базе данных',
        cors: 'Диапазон IP адресов',
        protocol: 'Сетевой протокол',
        current: 'Текущее значение: ',
        apply: 'Применить',
        show: 'Показать конфигурацию',
        reset: 'Сбросить',
        start: 'Запустить сервер',
        startAdmin: 'Запустить с правами администратора',
        createProfile: 'Создание профиля',
        updatePassword: 'Обновление пароля',
        deleteProfile: 'Удаление профиля',
        login: 'Логин:',
        password: 'Пароль:',
        oldPassword: 'Старый пароль:',
        newPassword: 'Новый пароль:',
        create: 'Создать',
        update: 'Обновить',
        delete: 'Удалить',
        ready: 'Готов к работе',
        currentConfig: 'Текущая конфигурация',
        close: 'Закрыть',
        saved: 'Конфигурация успешно обновлена',
        resetSuccess: 'Конфигурация сброшена',
        profileCreated: 'Профиль успешно добавлен',
        passwordUpdated: 'Пароль успешно обновлен',
        profileDeleted: 'Профиль удален',
        serverStarted: 'Сервер запущен',
        serverStartedAdmin: 'Сервер запущен с правами администратора',
        error: 'Ошибка',
        fillFields: 'Заполните все поля',
        portHelp: 'Номер сетевого порта для подключения к серверу.',
        dbHelp: 'Локальный путь к файлу базы данных.',
        corsHelp: 'Пулы IP-адресов разрешенные для подключения.',
        protocolHelp: 'Правила обмена данными между клиентом и сервером.'
    },
    en: {
        title: 'Server Configuration',
        tabConfig: 'Variables Definition',
        tabProfiles: 'Profile Management',
        port: 'Server Port',
        dbPath: 'Database Path',
        cors: 'IP Address Range',
        protocol: 'Network Protocol',
        current: 'Current value: ',
        apply: 'Apply',
        show: 'Show Configuration',
        reset: 'Reset',
        start: 'Start Server',
        startAdmin: 'Start as Administrator',
        createProfile: 'Create Profile',
        updatePassword: 'Update Password',
        deleteProfile: 'Delete Profile',
        login: 'Login:',
        password: 'Password:',
        oldPassword: 'Old Password:',
        newPassword: 'New Password:',
        create: 'Create',
        update: 'Update',
        delete: 'Delete',
        ready: 'Ready',
        currentConfig: 'Current Configuration',
        close: 'Close',
        saved: 'Configuration updated',
        resetSuccess: 'Configuration reset',
        profileCreated: 'Profile created',
        passwordUpdated: 'Password updated',
        profileDeleted: 'Profile deleted',
        serverStarted: 'Server started',
        serverStartedAdmin: 'Server started with admin privileges',
        error: 'Error',
        fillFields: 'Fill all fields'
    },
    de: {
        title: 'Server-Konfiguration',
        tabConfig: 'Variablendefinition',
        tabProfiles: 'Profilverwaltung',
        port: 'Server-Port',
        dbPath: 'Datenbankpfad',
        cors: 'IP-Adressbereich',
        protocol: 'Netzwerkprotokoll',
        current: 'Aktueller Wert: ',
        apply: 'Anwenden',
        show: 'Konfiguration anzeigen',
        reset: 'Zurücksetzen',
        start: 'Server starten',
        startAdmin: 'Als Administrator starten',
        createProfile: 'Profil erstellen',
        updatePassword: 'Passwort aktualisieren',
        deleteProfile: 'Profil löschen',
        login: 'Anmeldung:',
        password: 'Passwort:',
        oldPassword: 'Altes Passwort:',
        newPassword: 'Neues Passwort:',
        create: 'Erstellen',
        update: 'Aktualisieren',
        delete: 'Löschen',
        ready: 'Bereit',
        currentConfig: 'Aktuelle Konfiguration',
        close: 'Schließen',
        saved: 'Konfiguration aktualisiert',
        resetSuccess: 'Konfiguration zurückgesetzt',
        profileCreated: 'Profil erstellt',
        passwordUpdated: 'Passwort aktualisiert',
        profileDeleted: 'Profil gelöscht',
        serverStarted: 'Server gestartet',
        serverStartedAdmin: 'Server mit Admin-Rechten gestartet',
        error: 'Fehler'
    },
    fr: {
        title: 'Configuration Serveur',
        tabConfig: 'Définition des variables',
        tabProfiles: 'Gestion des profils',
        port: 'Port du serveur',
        dbPath: 'Chemin de la base',
        cors: 'Plage d\'adresses IP',
        protocol: 'Protocole réseau',
        current: 'Valeur actuelle: ',
        apply: 'Appliquer',
        show: 'Afficher la configuration',
        reset: 'Réinitialiser',
        start: 'Démarrer le serveur',
        startAdmin: 'Démarrer en tant qu\'admin',
        createProfile: 'Créer un profil',
        updatePassword: 'Mettre à jour le mot de passe',
        deleteProfile: 'Supprimer le profil',
        login: 'Identifiant:',
        password: 'Mot de passe:',
        oldPassword: 'Ancien mot de passe:',
        newPassword: 'Nouveau mot de passe:',
        create: 'Créer',
        update: 'Mettre à jour',
        delete: 'Supprimer',
        ready: 'Prêt',
        currentConfig: 'Configuration actuelle',
        close: 'Fermer',
        saved: 'Configuration mise à jour',
        resetSuccess: 'Configuration réinitialisée',
        profileCreated: 'Profil créé',
        passwordUpdated: 'Mot de passe mis à jour',
        profileDeleted: 'Profil supprimé',
        serverStarted: 'Serveur démarré',
        serverStartedAdmin: 'Serveur démarré avec droits admin',
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
    port: document.getElementById('port'),
    dbPath: document.getElementById('dbPath'),
    cors: document.getElementById('cors'),
    protocol: document.getElementById('protocol'),
    currentPort: document.getElementById('currentPort'),
    currentDbPath: document.getElementById('currentDbPath'),
    currentCors: document.getElementById('currentCors'),
    currentProtocol: document.getElementById('currentProtocol'),
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
function switchTab(tab) {
    document.querySelectorAll('.tab').forEach(t => t.classList.remove('active'));
    document.querySelectorAll('.tab-content').forEach(c => c.classList.remove('active'));
    if (tab === 'config') {
        document.querySelectorAll('.tab')[0].classList.add('active');
        document.getElementById('configTab').classList.add('active');
    } else {
        document.querySelectorAll('.tab')[1].classList.add('active');
        document.getElementById('profilesTab').classList.add('active');
    }
}
async function loadConfig() {
    try {
        const res = await fetch('/api/server-config');
        if (res.ok) {
            state.config = await res.json();
            updateCurrentValues();
        }
    } catch (e) {
        console.error('Ошибка загрузки:', e);
    }
}
function updateCurrentValues() {
    const prefix = translations[state.language].current || 'Текущее значение: ';
    elements.currentPort.textContent = prefix + (state.config.port || '');
    elements.currentDbPath.textContent = prefix + (state.config.connectionString || '');
    elements.currentCors.textContent = prefix + (state.config.ipAddressCors || '');
    elements.currentProtocol.textContent = prefix + (state.config.protocol || '');
}
async function applyConfig() {
    const config = {
        port: parseInt(elements.port.value) || 0,
        connectionString: 'Data Source=' + elements.dbPath.value,
        ipAddressCors: elements.cors.value,
        protocol: elements.protocol.value
    };
    try {
        const res = await fetch('/api/server-config', {
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
        const res = await fetch('/api/server-config/reset', { method: 'POST' });
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
async function startServer() {
    try {
        const res = await fetch('/api/server/start', { method: 'POST' });
        if (res.ok) {
            showToast(translations[state.language].serverStarted, 'success');
        } else {
            showToast(translations[state.language].error, 'error');
        }
    } catch (e) {
        showToast(`${translations[state.language].error}: ${e.message}`, 'error');
    }
}
async function startServerAsAdmin() {
    try {
        const res = await fetch('/api/server/start-admin', { method: 'POST' });
        if (res.ok) {
            showToast(translations[state.language].serverStartedAdmin, 'success');
        } else {
            showToast(translations[state.language].error, 'error');
        }
    } catch (e) {
        showToast(`${translations[state.language].error}: ${e.message}`, 'error');
    }
}
async function createProfile() {
    const login = document.getElementById('newLogin').value.trim();
    const password = document.getElementById('newPassword').value;
    if (!login || !password) {
        showToast(translations[state.language].fillFields, 'error');
        return;
    }
    try {
        const res = await fetch('/api/profiles/create', {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify({ login, password })
        });
        if (res.ok) {
            showToast(translations[state.language].profileCreated, 'success');
            document.getElementById('newLogin').value = '';
            document.getElementById('newPassword').value = '';
        } else {
            const err = await res.text();
            showToast(err || translations[state.language].error, 'error');
        }
    } catch (e) {
        showToast(`${translations[state.language].error}: ${e.message}`, 'error');
    }
}
async function updatePassword() {
    const login = document.getElementById('updateLogin').value.trim();
    const oldPassword = document.getElementById('oldPassword').value;
    const newPassword = document.getElementById('updateNewPassword').value;
    if (!login || !oldPassword || !newPassword) {
        showToast(translations[state.language].fillFields, 'error');
        return;
    }
    try {
        const res = await fetch('/api/profiles/update-password', {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify({ login, oldPassword, newPassword })
        });
        if (res.ok) {
            showToast(translations[state.language].passwordUpdated, 'success');
            document.getElementById('updateLogin').value = '';
            document.getElementById('oldPassword').value = '';
            document.getElementById('updateNewPassword').value = '';
        } else {
            const err = await res.text();
            showToast(err || translations[state.language].error, 'error');
        }
    } catch (e) {
        showToast(`${translations[state.language].error}: ${e.message}`, 'error');
    }
}
async function deleteProfile() {
    const login = document.getElementById('deleteLogin').value.trim();

    if (!login) {
        showToast(translations[state.language].fillFields, 'error');
        return;
    }
    if (!confirm(`Удалить профиль "${login}"?`)) return;
    try {
        const res = await fetch(`/api/profiles/${encodeURIComponent(login)}`, {
            method: 'DELETE'
        });
        if (res.ok) {
            showToast(translations[state.language].profileDeleted, 'success');
            document.getElementById('deleteLogin').value = '';
        } else {
            const err = await res.text();
            showToast(err || translations[state.language].error, 'error');
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
window.switchTab = switchTab;
window.toggleTheme = toggleTheme;
window.toggleLangDropdown = toggleLangDropdown;
window.setLanguage = setLanguage;
window.applyConfig = applyConfig;
window.showConfig = showConfig;
window.closeModal = closeModal;
window.resetConfig = resetConfig;
window.startServer = startServer;
window.startServerAsAdmin = startServerAsAdmin;
window.createProfile = createProfile;
window.updatePassword = updatePassword;
window.deleteProfile = deleteProfile;