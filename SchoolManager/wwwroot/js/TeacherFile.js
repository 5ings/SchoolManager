// Филтриране на учителите без презареждане на страницата
let debounceTimer = null;

async function loadTeachers() {
    // 1. Вземаме стойностите от полетата
    let search = document.getElementById("search").value;
    let subject = document.getElementById("subject").value;
    let sortOrder = document.getElementById("sortOrder").value;

    // 2. Подготвяме параметрите на заявката
    let params = new URLSearchParams({
        search: search,
        subject: subject,
        sortOrder: sortOrder
    });

    // 3. Изпращаме заявката към контролера
    let response = await fetch("/Teacher/GetFilterTeachers?" + params);

    // 4. Преобразуваме JSON отговора в JavaScript обекти
    let teachers = await response.json();

    // 5. Изчистваме старите редове от таблицата
    let tbody = document.getElementById("teachersTable");
    tbody.innerHTML = "";

    // 6. Добавяме ред за всеки намерен учител
    teachers.forEach(function (teacher) {
        let row = document.createElement("tr");

        [teacher.firstName, teacher.lastName,
        teacher.subject, teacher.schoolName].forEach(function (value) {
            let cell = document.createElement("td");
            cell.textContent = value ?? "";
            row.appendChild(cell);
        });

        // Колона с линкове за действия
        let actions = document.createElement("td");

        [
            { action: "Edit", text: "Редактирай" },
            { action: "Details", text: "Детайли" },
            { action: "Delete", text: "Изтрий" }
        ].forEach(function (item, index) {
            if (index > 0) {
                actions.appendChild(document.createTextNode(" | "));
            }

            let link = document.createElement("a");
            link.href = "/Teacher/" + item.action + "/" + teacher.id;
            link.textContent = item.text;
            actions.appendChild(link);
        });

        row.appendChild(actions);
        tbody.appendChild(row);
    });
}

// малко изчакване, за да не се праща заявка при всяка натисната клавиша
function loadTeachersDelayed() {
    clearTimeout(debounceTimer);
    debounceTimer = setTimeout(loadTeachers, 250);
}

// Автоматично филтриране
document.getElementById("search")
    .addEventListener("input", loadTeachersDelayed);

document.getElementById("subject")
    .addEventListener("input", loadTeachersDelayed);

document.getElementById("sortOrder")
    .addEventListener("change", loadTeachers);

// Изчистване на филтрите
document.getElementById("clearFilters")
    .addEventListener("click", function () {
        document.getElementById("search").value = "";
        document.getElementById("subject").value = "";
        document.getElementById("sortOrder").value = "name_asc";

        loadTeachers();
    });
