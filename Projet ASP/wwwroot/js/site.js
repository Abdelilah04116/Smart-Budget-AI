// SmartBudget AI - JavaScript principal

// Auto-hide alerts après 5 secondes
document.addEventListener('DOMContentLoaded', function () {
    const alerts = document.querySelectorAll('.alert:not(.alert-permanent)');
    alerts.forEach(alert => {
        setTimeout(() => {
            alert.style.transition = 'opacity 0.5s ease';
            alert.style.opacity = '0';
            setTimeout(() => alert.remove(), 500);
        }, 5000);
    });
});

// Confirmation avant suppression
function confirmDelete(event, message = 'Êtes-vous sûr de vouloir supprimer cet élément ?') {
    if (!confirm(message)) {
        event.preventDefault();
        return false;
    }
    return true;
}

// Formatage automatique des montants
function formatCurrency(amount) {
    return new Intl.NumberFormat('fr-FR', {
        style: 'currency',
        currency: 'EUR'
    }).format(amount);
}

// Validation côté client pour les formulaires
function validateTransactionForm() {
    const description = document.getElementById('descriptionInput');
    const amount = document.getElementById('amountInput');
    const category = document.getElementById('categoryInput');

    if (!description || !amount || !category) {
        return true; // Si les champs n'existent pas, laisser la validation serveur gérer
    }

    let isValid = true;
    let errorMessage = '';

    if (description.value.trim().length === 0) {
        errorMessage += '- La description est obligatoire\n';
        isValid = false;
    }

    if (amount.value === '' || isNaN(amount.value)) {
        errorMessage += '- Le montant doit être un nombre valide\n';
        isValid = false;
    }

    if (category.value.trim().length === 0) {
        errorMessage += '- La catégorie est obligatoire\n';
        isValid = false;
    }

    if (!isValid) {
        alert('Erreurs de validation:\n' + errorMessage);
    }

    return isValid;
}

// Loader pour requêtes AJAX
function showLoader() {
    const loader = document.createElement('div');
    loader.id = 'globalLoader';
    loader.className = 'position-fixed top-50 start-50 translate-middle';
    loader.style.zIndex = '9999';
    loader.innerHTML = '<div class="spinner-border text-primary" role="status"><span class="visually-hidden">Chargement...</span></div>';
    document.body.appendChild(loader);
}

function hideLoader() {
    const loader = document.getElementById('globalLoader');
    if (loader) {
        loader.remove();
    }
}

// Log pour debugging
console.log('✅ SmartBudget AI - JavaScript chargé avec succès');