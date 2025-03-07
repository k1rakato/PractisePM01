using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ConferencesOnInformationSecurity.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using ReactiveUI;

namespace ConferencesOnInformationSecurity.ViewModels
{
    /// <summary>
    /// ViewModel ��� ���������� ������������� � �� �����������.
    /// </summary>
    public class SharedViewModel : ViewModelBase
    {
        // ��������� ���� ��� �������� ������
        private List<Meropriatie> meropriaties;
        private List<Meropriatie> meropriaties0;
        private bool isVisible;
        private List<string> dateOfMeropriatie;
        private string selectDate;
        private List<Event> typeEvents;
        private Event selectedEvent;

        /// <summary>
        /// ������ ����������� � ������ ��������.
        /// </summary>
        public List<Meropriatie> Meropriaties { get => meropriaties; private set => this.RaiseAndSetIfChanged(ref meropriaties, value); }

        /// <summary>
        /// �������� ������ �����������.
        /// </summary>
        public List<Meropriatie> Meropriaties0 { get => meropriaties0; private set => this.RaiseAndSetIfChanged(ref meropriaties0, value); }

        /// <summary>
        /// ����������, ����� �� ������ �����������.
        /// </summary>
        public bool IsVisible { get => isVisible; private set => this.RaiseAndSetIfChanged(ref isVisible, value); }

        /// <summary>
        /// ������ ��������� ��� �����������.
        /// </summary>
        public List<string> DateOfMeropriatie { get => dateOfMeropriatie; private set => this.RaiseAndSetIfChanged(ref dateOfMeropriatie, value); }

        /// <summary>
        /// ��������� ���� �����������.
        /// </summary>
        public string SelectDate { get => selectDate; set { this.RaiseAndSetIfChanged(ref selectDate, value); Search(); } }

        /// <summary>
        /// ������ ��������� ����� �����������.
        /// </summary>
        public List<Event> TypeEvents { get => typeEvents; private set => this.RaiseAndSetIfChanged(ref typeEvents, value); }

        /// <summary>
        /// ��������� �����������.
        /// </summary>
        public Event SelectedEvent { get => selectedEvent; set { this.RaiseAndSetIfChanged(ref selectedEvent, value); Search(); } }

        /// <summary>
        /// �����������, ����������� ������������� ������.
        /// </summary>
        public SharedViewModel()
        {
            InitializeData();
        }

        /// <summary>
        /// ������������� ������ ViewModel.
        /// </summary>
        private void InitializeData()
        {
            meropriaties0 = db.Meropriaties.Include(x => x.Event).ToList();
            meropriaties = meropriaties0;
            typeEvents = db.Events.ToList();
            typeEvents.Add(new Event() { Idevent = 0, Eventname = "�������� ��� �����������" });
            typeEvents = typeEvents.OrderBy(x => x.Idevent).ToList();
            selectedEvent = typeEvents.FirstOrDefault(x => x.Idevent == 0);
            isVisible = false;
            dateOfMeropriatie = meropriaties0.Select(x => x.Datemeropriatie.ToString()).Distinct().Order().ToList();
            dateOfMeropriatie.Insert(0, "�������� ����");
            selectDate = dateOfMeropriatie[0];
        }

        /// <summary>
        /// ���������� ������ ����������� �� ���������� ���� �/��� ����.
        /// </summary>
        private void Search()
        {
            Meropriaties = meropriaties0;

            bool isEventSelected = SelectedEvent?.Idevent != 0;
            bool isDateSelected = SelectDate != "�������� ����";

            if (isEventSelected && isDateSelected)
            {
                Meropriaties = meropriaties0.Where(x => x.Eventid == SelectedEvent.Idevent && x.Datemeropriatie.ToString() == SelectDate).ToList();
            }
            else if (isEventSelected)
            {
                Meropriaties = meropriaties0.Where(x => x.Eventid == SelectedEvent.Idevent).ToList();
            }
            else if (isDateSelected)
            {
                Meropriaties = meropriaties0.Where(x => x.Datemeropriatie.ToString() == SelectDate).ToList();
            }

            IsVisible = Meropriaties.Count == 0;
        }

        /// <summary>
        /// ����� ��� ��������� � ���� �����������.
        /// </summary>
        public void NavigateToAuth()
        {
            MainWindowViewModel.Self.Uc = new AauthView();
        }
    }
}