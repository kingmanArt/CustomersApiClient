   M S B A M L   `   `   `  ����c  _System.Windows.Interactivity, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35qPclr-namespace:System.Windows.Interactivity;assembly=System.Windows.InteractivitySystem.Windows.Interactivity  5   	   M ICustomersAPIClient, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null7 clr-namespace:CustomersAPIClientCustomersAPIClient 5   	   #   CustomersAPIClient.MainWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   \iPclr-namespace:System.Windows.Interactivity;assembly=System.Windows.Interactivity    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   2local clr-namespace:CustomersAPIClient     5   	     � Title$  
MainWindow��5	   	    � WindowState$ 	Maximized=�6   .��5      �  � RowDefinitions 5   
   �� 5      $��40 �6   �� 5      $��* �6    � ColumnDefinitions 5   
   �� 5      $��5* �6   �� 5      $��1* �6   �� 5      $
��1.3* �6   .+�5   
   �� $��0��6   $��1��6!    �� SelectedIndex$ 0��5      $��Center=�5      $��Right=�5      $��70��5       �� SelectedItem 5      �� *SelectedDataSource+ �� UpdateSourceTrigger$ PropertyChanged=���5      �� *LanguagesCombobox+5      �� 5"   
   $��0��5#      $��Right=�5$      $��Center=�5%      $��80��5&      ��5"      ��  �� Path$ FastData�$ PropertyChanged=�.��5&       ��5'      O Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem 5)      �� 5(      6sys&clr-namespace:System;assembly=mscorlib     5)       �� 
TargetType" ��5(      	 �� 	Resources	 5*         CueBannerBrush
 j� 
AlignmentX j� 
AlignmentY j� Stretch !� Visual��%�   &	        !� 5+      $

 Left=�5,   "   $ Center=�5-   "   $
 None=�66    5.      �� 5/      $��Search��6$   
�������50   $   52      53       �� Triggers 54      P� 55       P� Property � �� �� Empty P� Value# Z 56      . �57      ��  �� Property � �� �� Value# [  58   !   59      P� 5:       � �� 5;      Z� . �5<      ��  � ��# [  5=   !   5>      P� 5?       �@J�IsKeyboardFocused . . �5A      ��  � ��
 �����5B   !   5C      5E      .��5F      5G      �� 5I   
   $��Pacients��6   $��0��5J      $
��Left=�5K      $	��Top=�5L      ' !CustomersAPIClient.CustomDataGrid  5M   
   -   5          Name$ 
persongrid��5M      $��1��5N      &   System.Windows.Controls.DataGrid   AutoGenerateColumns .  $��Stretch=�5P      $��Stretch=�5Q         AlternatingRowBackground
 �����   CanUserDeleteRows .     CanUserAddRows .     SelectionMode$ Extended=�5V      $��3��5Y   y   $��0,0,-0.4,-0.4q�6�    5W      �� *SelectePerson+$ PropertyChanged=�   SelectedItemsList 5X      �� *TestSelected+ �� Mode$ TwoWay=�$ PropertyChanged=���5Y      �� *FiltredPersons+$ TwoWay=�$ PropertyChanged=�   Columns 5Z      0  *System.Windows.Controls.DataGridTextColumn  5[      ,  &System.Windows.Controls.DataGridColumn   Header$ 	Adress No��6%      Width5  /System.Windows.Controls.DataGridLengthConverter$ * 5\   %   1  +System.Windows.Controls.DataGridBoundColumn   Binding 5]   %   �� *Id+  5^      $ Company��6%   $ * 5_   %    5`   %   �� *Cpny+$ PropertyChanged=�  5a      $ Greeting��6%   $ * 5b   %    5c   %   �� *	GrTxtEdit+$ PropertyChanged=�  5d      $ Title��6%   $ * 5e   %    5f   %   �� *Title+  5g      $ 
First Name��6%   $ * 5h   %    5i   %   �� *Fname+  5j      $ 	Last Name��6%   $ * 5k   %    5l   %   �� *Lname+  5m      $ Street��6%   $ * 5n   %    5o   %   �� *Street+  5p      $ Country��6%   $ * 5q   %    5r   %   �� *
ConTxtEdit+$ PropertyChanged=�  5s      $ Postal Code��6%   $ * 5t   %    5u   %   �� *Zip+  5v      $
 City��6%   $ * 5w   %    5x   %   �� *City+  5y      $ Phone��6%   $ * 5z   %    5{   %   �� *Test+$ PropertyChanged=�  5|      $ Date of Birth��6%   $ * 5}   %    5~   %   �� *DateOfBirth+  5      $ Registration��6%   $ * 5�   %    5�   %   �� *FirstContact+5�      `� 5�   
   $��Right=�6     `� 
Background
  �����$	��Top=�5�      $��2��5�      $��0��5�      $��45��5�      .�5�      c� 
�������$��
0,0,20,465q�5�      .@�5�      �� ! � ToolTip$	! New��6   ��5�      �� *
NewCommand+.��5�      �� $��Images/New.jpg��6   $��20��5�      $��30��5�      5�      �� 5�      $
! Edit��6   " �� 	IsDefault" .  ��5�      �� *EditCommand+{�5�      �� *EditButtonOfforOn+$ PropertyChanged=�.��5�      �� $��Images/Edit.png��6   $��20��5�      5�      �� 5�      $! Refresh��6   ��5�      �� *
RefCommand+.��5�      �� $��Images/refresh.png��6   $��20��5�      5�      �� 5�      $! Delete��6   " .  ��5�      �� *
DelCommand+{�5�      �� *DeleteButtonOfforOn+$ PropertyChanged=�.��5�      �� $��Images/Delete.png��6   $��20��5�      5�      5�      5�      5�      5�      